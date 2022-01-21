using System;
using IntParse;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please enter a number: ");
            var numberString = Console.ReadLine();
            var parseResult = numberString.TryParseInt32(out int numberInt);

            if (parseResult)
            {
                Console.WriteLine($"Convert the number {numberInt} is successfull");
            }
            else
            {
                Console.WriteLine("Failed to convert the number");
            }

            Console.ReadKey();
        }
    }
}
