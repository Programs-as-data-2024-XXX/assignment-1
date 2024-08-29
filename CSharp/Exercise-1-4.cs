using System;

namespace Exercise14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number: ");
            int num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the power: ");
            int power = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The result is: " + Math.Pow(num, power));
        }
    }
} 