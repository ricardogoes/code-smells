using System;
using System.Linq;
using System.Net.Mail;

namespace CodeSmells.Couplers
{
    public class HiddenDependencies
    {
        public void Checkout(Cart cart, PaymentInfo paymentInfo)
        {
            // process payment

            var client = new SmtpClient("localhost");
            var message = new MailMessage("sender@example.com", "recipient@example.com", "Receipt", "Thanks for buying!");
            client.Send(message);
        }
    }

    public class Cart
    {
    }

    public class PaymentInfo
    {
        public string CustomerEmailAddress { get; set; }
    }
}
