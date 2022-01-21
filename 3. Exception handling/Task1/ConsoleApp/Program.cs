using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string");

            char firstSymbol;

            do
            {
                firstSymbol = '\0';
                var s = Console.ReadLine();


                if (!string.IsNullOrEmpty(s))
                {
                    firstSymbol = s.First();
                }

                if (firstSymbol != '\0')
                {
                    Console.WriteLine($"First symbol of the string: {firstSymbol}");
                }

            } while (firstSymbol != '\0');

            Console.WriteLine("String is empty");

            Console.ReadKey();
        }
    }
}
