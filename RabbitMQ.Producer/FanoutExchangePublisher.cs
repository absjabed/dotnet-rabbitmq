using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;
namespace RabbitMQ.Producer
{
    public static class FanoutExchangePublisher
    {
        public static void Publish(IModel channel){
            /**Life time of a message**/
            var ttl = new Dictionary<string, object>{
              {"x-message-ttl", 30000}  
            };
            channel.ExchangeDeclare("demo-fanout-exchange-kds", ExchangeType.Fanout, arguments: ttl);
            var count = 0;
            while(true){
                var message = new {count};
                    //new { Name = "From Producer", Message = "Hello! "+ i};
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                    
                    var properties = channel.CreateBasicProperties();
                    properties.Headers = new Dictionary<string, object>{ { "account", "new" } };

                    channel.BasicPublish("demo-fanout-exchange-kds", string.Empty, properties, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}