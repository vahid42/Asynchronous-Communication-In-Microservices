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
        private readonly INotificationService notificationService;

        public MessageConsumerService(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ConnectionFactory conFactory = new();
            conFactory.Uri = new Uri(MessageConstant.url);
            conFactory.ClientProvidedName = "Notification Api";
            using IConnection connection = await conFactory.CreateConnectionAsync();
            using IChannel channel = await connection.CreateChannelAsync();
            await channel.ExchangeDeclareAsync(MessageConstant.ExchangeName, ExchangeType.Direct, cancellationToken: stoppingToken);
            await channel.QueueDeclareAsync(MessageConstant.Queuename, false, false, false, null, cancellationToken: stoppingToken);
            await channel.QueueBindAsync(MessageConstant.Queuename, MessageConstant.ExchangeName, MessageConstant.RoutingKey, null, cancellationToken: stoppingToken);
            await channel.BasicQosAsync(0, 1, false, stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, args) =>
            {
                var body = args.Body.ToArray();
                string json = Encoding.UTF8.GetString(body);

                var notiy = JsonSerializer.Deserialize<GetOrderInQueueDto>(json);
                if (notiy != null)
                {
                    var notification = new API.Entities.Notification()
                    {
                        Email = notiy.CustomerEmail,
                        OrderDeatils = notiy.Id + "-" + notiy.Amount + "-" + notiy.Currency,
                        SendDateTime = DateTime.Now
                    };
                    await notificationService.CreateNotificationAsync(notification);
                    await channel.BasicConsumeAsync(MessageConstant.Queuename, true, consumer);
                }

            };

        }
    }
}
