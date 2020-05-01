using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WSS_first_task
{
    class Program
    {
        static void Main(string[] args)
        {
            int sizeOfArr = SnailMatrix.setValidLength();
            int[,] arr = new int[sizeOfArr, sizeOfArr];

            arr = SnailMatrix.snailAlgorithm(arr, SnailMatrix.fillMatrix);
            Console.WriteLine("\nMatrix: ");
            SnailMatrix.printMatrix(arr);

            Console.WriteLine("\n\nReading a matrix in <snail> way: ");
            arr = SnailMatrix.snailAlgorithm(arr, SnailMatrix.printSnailedMatrix);

            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadKey(true);
            return;
        }
    }
}
