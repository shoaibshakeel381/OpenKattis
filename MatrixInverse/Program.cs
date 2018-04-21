using System;
using MatrixInverse.Kattis.IO;

namespace MatrixInverse
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var scanner = new Scanner();
            
            var index = 0;
            while (scanner.HasNext())
            {
                // Take Input
                var matrixElements = new int[4];
                for (var i = 0; i < 4; i++)
                {
                    matrixElements[i] = scanner.NextInt();
                }
                
                var matrix = new Matrix(matrixElements[0], matrixElements[1], matrixElements[2], matrixElements[3]);

                // Inverse Matrix
                matrix = matrix.Inverse();

                // Print Matrix
                Console.WriteLine($"Case {++index}:");
                Console.WriteLine(matrix.ToString());
            }
        }
    }
}
