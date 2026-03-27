using System;

namespace Adapter
{
    interface IProcessPayment
    {
        void PayIntent(float amount);
    }

    class StripePayment
    {
        private readonly Random rnd = new Random();
        public string CreateIntent(float amount)
        {
            int enc = rnd.Next(1, 100);
            string intent = (enc * amount).ToString();
            return intent;
        }

        public bool Pay(string intent)
        { return true; }
    }

    class StripeAdapter : IProcessPayment
    {
        private readonly StripePayment _stripe;

        public StripeAdapter(StripePayment stripe)
        { _stripe = stripe; }

        public void PayIntent(float amount)
        {
            string intent = _stripe.CreateIntent(amount);
            if (_stripe.Pay(intent))
            { Console.WriteLine("Payment Success"); }
        }
    }

    class PaymentModule
    {
        private IProcessPayment Merchant;

        public PaymentModule(IProcessPayment merchant)
        { Merchant = merchant; }

        public void SetMerchant(IProcessPayment merchant)
        { Merchant = merchant; }

        public void Pay(float amount)
        {
            Console.WriteLine($"Processing Payment of {amount}");
            Merchant.PayIntent(amount);
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            IProcessPayment adapter = new StripeAdapter(new StripePayment());
            PaymentModule module = new PaymentModule(adapter);
            module.Pay(99.99f);
        }
    }
}