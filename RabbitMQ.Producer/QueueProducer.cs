using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Threading;

namespace RabbitMQ.Producer
{
   public static class QueueProducer
    {
       public static void Publish(IModel channel)
        {
            channel.QueueDeclare("demo-queue-kds",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
            var count = 0;

            while(true){
                var message = new {count};
                    //new { Name = "From Producer", Message = "Hello! "+ i};
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                    channel.BasicPublish("", "demo-queue-kds", null, body);
                count++;
                //Thread.Sleep(1000);
            }
        }
    }
}
