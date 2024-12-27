using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks.Dataflow;
using SheardModel;
using RabbitMQ.Client.Events;

namespace ReceiverMessageTow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Receiver Tow ...";
            Console.WriteLine(" Start Receiving Messages? Yes/No ");
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
                    Task.Run(() => Utilites.ProcessMessage(OperationType.Receive, "Rabbitmq Receive Tow Message App")).Wait();
                    break;
                default:
                    break;
            }
        }

       
       
    }
}