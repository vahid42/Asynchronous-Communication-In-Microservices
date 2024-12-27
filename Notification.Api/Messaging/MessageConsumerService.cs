using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Notification.API.Constants;
using System.Text;
using Notification.API.Services;
using System.Text.Json;
using Notification.API.Dtos;
using Notification.Api.Dtos;

namespace Notification.Api.Messaging
{
    public class MessageConsumerService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private IChannel channel;
        private IConnection connection;
        public MessageConsumerService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ConnectionFactory conFactory = new()
            {
                Uri = new Uri(MessageConstant.url),
                ClientProvidedName = "Notification Api"
            };

            connection = await conFactory.CreateConnectionAsync();
            channel = await connection.CreateChannelAsync();
            await channel.ExchangeDeclareAsync(MessageConstant.ExchangeName, ExchangeType.Direct, cancellationToken: stoppingToken);
            await channel.QueueDeclareAsync(MessageConstant.Queuename, false, false, false, null, cancellationToken: stoppingToken);
            await channel.QueueBindAsync(MessageConstant.Queuename, MessageConstant.ExchangeName, MessageConstant.RoutingKey, null, cancellationToken: stoppingToken);
            await channel.BasicQosAsync(0, 1, false, stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, args) =>
            {
                var body = args.Body.ToArray();
                string json = Encoding.UTF8.GetString(body);
                var notify = JsonSerializer.Deserialize<GetOrderInQueueDto>(json);
                if (notify != null)
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                        var notification = new API.Entities.Notification()
                        {
                            Email = notify.CustomerEmail,
                            OrderDeatils = $"{notify.Id}-{notify.Price}-{notify.Prudoct}-{notify.CustomerFullName}",
                            SendDateTime = DateTime.Now
                        };
                        await notificationService.CreateNotificationAsync(notification);
                    }
                    await channel.BasicAckAsync(args.DeliveryTag, false);
                    Console.WriteLine(json);
                }
                else
                {
                    await channel.BasicNackAsync(args.DeliveryTag, false, false); // Reject message without requeue  
                }
            };

            // Start consuming messages  
            await channel.BasicConsumeAsync(queue: MessageConstant.Queuename, autoAck: false, consumer: consumer);
        }


        public override void Dispose()
        {
            base.Dispose();
            channel.Dispose();
            connection.Dispose();
        }
    }
}

