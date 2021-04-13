using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Consumer
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
            
            /**
                ONCE CHANNEL IS CREATED WE DONT NEED BELOW LINE
                TO CREATE NEW CHANNEL..AS IT'S ALREADY BEEN CREATED.
            **/
            // channel.QueueDeclare("demo-queue-kds",
            //         durable: true,
            //         exclusive: false,
            //         autoDelete: false,
            //         arguments: null);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                 Console.WriteLine(message);
            };

            channel.BasicConsume("demo-queue-kds", true, consumer);
            Console.ReadLine();
        }
    }
}
