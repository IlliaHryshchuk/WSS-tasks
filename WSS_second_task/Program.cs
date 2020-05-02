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
            SnailMatrix.readFromDB();

            SnailMatrix sm = new SnailMatrix(SnailMatrix.setValidSize());
            sm.fillInSnailWay();
            Console.WriteLine("\nMatrix: ");
            sm.printMatrix();

            Console.WriteLine("\nReading a matrix in <snail> way: ");
            sm.printInSnailWay();

            sm.writeToDB();

            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadKey(true);
            return;
        }
    }
}