using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WSS_first_task
{
    class Program
    {
        delegate int Action(ref int[,] arr, int i, int j, int num);


        static void printMatrix(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    Console.Write(arr[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }


        static int fillMatrix(ref int[,] arr, int i, int j, int num)
        {
            arr[i,j] = num;
            num++;
            return num;
        }


        static int printSnailedMatrix(ref int[,] arr, int i, int j, int num)
        {
            Console.Write(arr[i, j] + " ");
            return 1;
        }


        static int[,] snailAlgorithm(int[,] arr, Action func)
        {
            int j = 0, num = 0;
            for (int i = 1; i <= arr.GetLength(0) / 2; i++)
            {
                for (j = i - 1; j < arr.GetLength(0) - i + 1; j++)
                    num = func(ref arr, i - 1, j, num);
                
                for (j = i; j < arr.GetLength(0) - i + 1; j++)
                    num = func(ref arr, j, arr.GetLength(0) - i, num);
                
                for (j = arr.GetLength(0) - i - 1; j >= i - 1; --j) 
                    num = func(ref arr, arr.GetLength(0) - i, j, num);
                
                for (j = arr.GetLength(0) - i - 1; j >= i; j--)
                    num = func(ref arr, j, i - 1, num);
            }
            if (arr.GetLength(0) % 2 == 1)
                num = func(ref arr, arr.GetLength(0) / 2, arr.GetLength(0) / 2, num);

            return arr;
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Enter a size of matrix: ");
            int sizeOfArr = int.Parse(Console.ReadLine());
            int[,] arr = new int[sizeOfArr, sizeOfArr];
            Action action;


            action = fillMatrix;
            arr = snailAlgorithm(arr, action);
            Console.WriteLine("\nMatrix: ");
            printMatrix(arr);


            Console.WriteLine("\n\nReading a matrix in <snail> way: ");
            action = printSnailedMatrix;
            arr = snailAlgorithm(arr, action);


            Console.WriteLine();
        }
    }
}
