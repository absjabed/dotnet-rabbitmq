using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Consumer
{
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel){
            channel.ExchangeDeclare("demo-direct-exchange-kds", ExchangeType.Direct);
            channel.QueueDeclare("demo-direct-queue-kds",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            /**Bind Exchange => Queue => with routing key**/
            channel.QueueBind("demo-direct-queue-kds", "demo-direct-exchange-kds", "account.init");
            /**Pre-Fetch Count**/
            channel.BasicQos(0, 10, false);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                 Console.WriteLine(message);
            };

            channel.BasicConsume("demo-direct-queue-kds", true, consumer);
            Console.WriteLine("Consumer Started.");
            Console.ReadLine();
        }
    }
}