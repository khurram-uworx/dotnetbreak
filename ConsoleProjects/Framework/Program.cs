using System;
using System.Messaging;

namespace Framework
{
    public class Transaction
    {
        public int Account { get; set; }
        public int Deposit { get; set; }
        public DateTime DepositDate { get; set; }
    }

    internal class Program
    {
        static void sendMSMQMessage()
        {
            var message = new Message();
            message.Body = new Transaction
            {
                Account = 10001,
                Deposit = 500,
                DepositDate = DateTime.Now
            };

            var queue = new MessageQueue(".\\Private$\\deposits");
            queue.Send(message);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
