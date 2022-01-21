using System;
using System.Linq;

namespace HelloWorld.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            string name;
            if (args.Length == 0)
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine();
            }
            else
            {
                name = args.FirstOrDefault();
            }

            var message = HelloWorld.Library.HelloWorld.GetHello(name);

            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
