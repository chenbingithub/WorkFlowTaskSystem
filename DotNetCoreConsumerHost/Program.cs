using System;

namespace DotNetCoreConsumerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var bs = new DotNetCoreConsumerHostBootstrap();
            bs.Start();

            Console.ReadLine();
        }
    }
}
