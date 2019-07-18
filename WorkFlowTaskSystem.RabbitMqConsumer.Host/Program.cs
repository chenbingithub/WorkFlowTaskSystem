using System;

namespace WorkFlowTaskSystem.RabbitMqConsumer.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var bs = new WorkFlowTaskSystemConsumerHostBootstrap();
            bs.Start();

            Console.ReadLine();
        }
    }
}
