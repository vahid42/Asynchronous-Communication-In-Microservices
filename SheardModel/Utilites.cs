using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SheardModel
{
    public static class Utilites
    {
        private static readonly string[] firstNames = { "John", "Paul", "Ringo", "George","Vahid" };
        private static readonly string[] lastNames = { "Lennon", "McCartney", "Starr","Ghamari" ,"Harrison" };
        public static string GenerateName()
        {
            var random = new Random();
            string firstName = firstNames[random.Next(0, firstNames.Length)];
            string lastName = lastNames[random.Next(0, lastNames.Length)];

            return $"{firstName} {lastName}";
        }


        public static async Task ProcessMessage(OperationType operationType, string clientProvidedName)
        {
            if(operationType==OperationType.Send)
            {
               await  ProcessSendMessage(clientProvidedName);
            }
            else if (operationType == OperationType.Receive)
            {
                await ProcessReceievMessage(clientProvidedName);
            }
        }
        private static async Task ProcessSendMessage(string clientProvidedName)
        {
            ConnectionFactory conFactory = new();
            conFactory.Uri = new Uri(ConstantDemo.url); 
            conFactory.ClientProvidedName = clientProvidedName;
            IConnection connection = await conFactory.CreateConnectionAsync();

            IChannel channel = await connection.CreateChannelAsync();
            await channel.ExchangeDeclareAsync(ConstantDemo.ExchangeName, ExchangeType.Direct);
            await channel.QueueDeclareAsync(ConstantDemo.Queuename, false, false, false, null);
            await channel.QueueBindAsync(ConstantDemo.Queuename, ConstantDemo.ExchangeName, ConstantDemo.RoutingKey, null);

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(1000);

                var message = new Item(i, Utilites.GenerateName());
                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);
                await channel.BasicPublishAsync(ConstantDemo.ExchangeName, ConstantDemo.RoutingKey, body);
                var select = i % 2;
                if (select == 0)
                    Console.ForegroundColor = ConsoleColor.Blue;
                else
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($"Send Message {i} : {json}");

            }
            await channel.CloseAsync();
            await connection.CloseAsync();
        }
        private static async Task ProcessReceievMessage(string clientProvidedName)
        {
            ConnectionFactory conFactory = new();
            conFactory.Uri = new Uri(ConstantDemo.url); 
            conFactory.ClientProvidedName = clientProvidedName;
            IConnection connection = await conFactory.CreateConnectionAsync();

            IChannel channel = await connection.CreateChannelAsync();
            await channel.ExchangeDeclareAsync(ConstantDemo.ExchangeName, ExchangeType.Direct);
            await channel.QueueDeclareAsync(ConstantDemo.Queuename, false, false, false, null);
            await channel.QueueBindAsync(ConstantDemo.Queuename, ConstantDemo.ExchangeName, ConstantDemo.RoutingKey, null);
            await channel.BasicQosAsync(0, 1, false);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, args) =>
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                var body = args.Body.ToArray();
                string json = Encoding.UTF8.GetString(body);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(json);
                await channel.BasicAckAsync(args.DeliveryTag, false);
            };

            string consumerTag = await channel.BasicConsumeAsync(ConstantDemo.Queuename, false, consumer);
            Console.ReadLine();
            await channel.BasicCancelAsync(consumerTag);
            await channel.CloseAsync();
            await connection.CloseAsync();
        }


    }
}
