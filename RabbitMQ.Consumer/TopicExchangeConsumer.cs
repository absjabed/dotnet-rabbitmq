using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace RabbitMQ.Consumer
{
    public static class TopicExchangeConsumer
    {
        public static void Consume(IModel channel){
            channel.ExchangeDeclare("demo-topic-exchange-kds", ExchangeType.Topic);
            channel.QueueDeclare("demo-topic-queue-kds",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            /**Bind Exchange => Queue => with routing key**/
            channel.QueueBind("demo-topic-queue-kds", "demo-topic-exchange-kds", "account.*");
            /**Pre-Fetch Count**/
            channel.BasicQos(0, 10, false);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                 Console.WriteLine(message);
            };

            channel.BasicConsume("demo-topic-queue-kds", true, consumer);
            Console.WriteLine("Consumer Started.");
            Console.ReadLine();
        }
    }
}