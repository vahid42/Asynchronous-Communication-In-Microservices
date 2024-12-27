using RabbitMQ.Client;
using SheardModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SenderMassege
{
    internal class Program
    {


        static void Main(string[] args)
        {
            Console.Title = "Sender Message ...";

            Console.WriteLine(" Start Sending Messages? Yes/No ");
            var input = Console.ReadLine();
            Operation(input);
            Console.ReadKey();
        }


        private static void Operation(string? input)
        {
            switch (input?.ToLower())
            {
                case null:
                    break;
                case "no":
                    break;
                case "yes":
                    Task.Run(() => Utilites.ProcessMessage(OperationType.Send, "Rabbitmq Send Message App"));
                    break;
                default:
                    break;
            }
        }


    }
}