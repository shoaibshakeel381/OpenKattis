using System;

namespace PizzaCrust
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ');

            int r = int.Parse(input[0]);
            int c = int.Parse(input[1]);

            // TotalPizzaArea = PI * r ^ 2
            // TotalCrustArea = PI * (r ^ 2 - (r - c) ^ 2)
            // TotalCheeseArea = TotalPizzaArea - TotalCrustArea
            //                 = PI * r ^ 2 - PI * (r ^ 2 - (r - c) ^ 2)
            //                 = PI * (r - c) ^ 2

            // TotalCheeseAreaPercentage = TotalCheeseArea / TotalPizzaArea * 100
            //                           = (PI * (r - c) ^ 2) / (PI * r ^ 2) * 100
            //                           = ((r - c) ^ 2) / (r ^ 2) * 100

            var totalCheeseAreaPercentage = ((double)((r - c) * (r - c))) / (r * r) * 100;

            Console.WriteLine($"{totalCheeseAreaPercentage:0.000000}");
        }
    }
}
