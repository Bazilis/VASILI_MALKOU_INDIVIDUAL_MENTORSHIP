using BLL.Interfaces;
using System;
using System.Net;
using System.Net.Mail;

namespace BLL.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly IRabbitmqProducer _producer;
        private readonly IRabbitmqConsumer _consumer;

        public EmailSenderService(IRabbitmqProducer producer, IRabbitmqConsumer consumer)
        {
            _producer = producer;
            _consumer = consumer;
        }

        public (bool, string) SendEmail(string userEmail, string emailSubject, string emailBody, bool isUseRabbitmq)
        {
            if (!isUseRabbitmq)
                return SendEmailTo(userEmail, emailSubject, emailBody);

            var messageToRabbit = emailSubject + ";" + emailBody;

            var producerResult = _producer.Produce(messageToRabbit);

            if (!producerResult.Item1)
                return (false, producerResult.Item2);

            var messageFromRabbit = _consumer.Consume();

            var words = messageFromRabbit.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            return SendEmailTo(userEmail, words[0], words[1]);
        }

        private (bool, string) SendEmailTo(string userEmail, string emailSubject, string emailBody)
        {
            var mail = new MailMessage("WeatherForecastInfoBot@gmail.com", userEmail, emailSubject, emailBody);

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("WeatherForecastInfoBot@gmail.com", "12345678")
            };

            try
            {
                client.Send(mail);

                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
