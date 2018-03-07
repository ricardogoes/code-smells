using System;
using System.Linq;
using System.Net.Mail;

namespace CodeSmells.Couplers.Refactored
{
    public class HiddenDependencies
    {
        private readonly INotifyCustomer _notificationService;

        public HiddenDependencies(INotifyCustomer notificationService)
        {
            this._notificationService = notificationService;
        }

        public void Checkout(Cart cart, PaymentInfo paymentInfo)
        {
            // process payment

            _notificationService.SendMessage("sender@example.com", "recipient@example.com", "Receipt", "Thanks for buying!");
        }
    }

    public class Cart
    {
    }

    public class PaymentInfo
    {
        public string CustomerEmailAddress { get; set; }
    }

    public interface INotifyCustomer
    {
        void SendMessage(string from, string to, string subject, string body);
    }

    public class SmtpNotifier : INotifyCustomer
    {
        public void SendMessage(string from, string to, string subject, string body)
        {
            var client = new SmtpClient("localhost");
            var message = new MailMessage(from, to, subject, body);
            client.Send(message);
        }
    }
}