using BLL.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Text;

namespace BLL.Services
{
    public class RabbitmqProducerService : IRabbitmqProducer
    {
        public (bool, string) Produce(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ConfirmSelect();

                channel.QueueDeclare(queue: "email_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);
                try
                {
                    channel.BasicPublish(exchange: "",
                                     routingKey: "email_queue",
                                     basicProperties: null,
                                     body: body);
                    channel.WaitForConfirmsOrDie(new TimeSpan(0, 0, 3));
                }
                catch(OperationInterruptedException ex)
                {
                    return (false, ex.Message);
                }
            }

            return (true, "");
        }
    }
}
