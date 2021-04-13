using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Threading;

namespace RabbitMQ.Producer
{
   static class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory 
            {
                Uri = new Uri("amqp://guest:guest@192.168.99.100:5672")
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            DirectExchangePublisher.Publish(channel); 
        }
    }
}
