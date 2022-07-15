using BLL.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;

namespace BLL.Services
{
    public class RabbitmqConsumerService : IRabbitmqConsumer
    {
        public string Consume()
        {
            string _message = string.Empty;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "email_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    _message = message;
                };

                channel.BasicConsume(queue: "email_queue",
                                     autoAck: true,
                                     consumer: consumer);

                Thread.Sleep(1);
            }

            return _message;
        }
    }
}
