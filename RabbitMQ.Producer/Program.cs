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
            channel.QueueDeclare("demo-queue-kds",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            for(var i = 1; i<=1000; i++ ){

                 Thread.Sleep(100);

                    var message = new {Count = "a"+i};
                    //new { Name = "From Producer", Message = "Hello! "+ i};
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                    channel.BasicPublish("", "demo-queue-kds", null, body);

                Thread.Yield();
            }
        }
    }
}
