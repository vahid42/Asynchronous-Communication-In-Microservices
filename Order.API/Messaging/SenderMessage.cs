using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Order.API.Constants;

namespace Order.API.Messaging
{
    public class SenderMessage : ISenderMessage
    {
        public async Task<bool> SendMessage(object message)
        {
            try
            {
                ConnectionFactory conFactory = new();
                conFactory.Uri = new Uri(MessageConstant.url);
                conFactory.ClientProvidedName = "Order Api";
                using IConnection connection = await conFactory.CreateConnectionAsync();
                using IChannel channel = await connection.CreateChannelAsync();
                await channel.ExchangeDeclareAsync(MessageConstant.ExchangeName, ExchangeType.Direct);
                await channel.QueueDeclareAsync(MessageConstant.Queuename, false, false, false, null);
                await channel.QueueBindAsync(MessageConstant.Queuename, MessageConstant.ExchangeName, MessageConstant.RoutingKey, null);
                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);
                await channel.BasicPublishAsync(MessageConstant.ExchangeName, MessageConstant.RoutingKey, body);
                Console.WriteLine($"Send Message => : {json}");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
