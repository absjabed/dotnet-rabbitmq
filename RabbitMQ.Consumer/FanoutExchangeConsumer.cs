using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace RabbitMQ.Consumer
{
    public static class FanoutExchangeConsumer
    {
        public static void Consume(IModel channel){
            channel.ExchangeDeclare("demo-fanout-exchange-kds", ExchangeType.Fanout);
            channel.QueueDeclare("demo-fanout-queue-kds",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            
            /**Bind Exchange => Queue => with routing key**/
            channel.QueueBind("demo-fanout-queue-kds", "demo-fanout-exchange-kds", string.Empty);
            /**Pre-Fetch Count**/
            channel.BasicQos(0, 10, false);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                 Console.WriteLine(message);
            };

            channel.BasicConsume("demo-fanout-queue-kds", true, consumer);
            Console.WriteLine("Consumer Started.");
            Console.ReadLine();
        }
    }
}