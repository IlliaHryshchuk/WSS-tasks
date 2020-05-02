using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WSS_first_task
{
    class SnailMatrix
    {
        [Key]
        public int Id { get; set; }
        public int[,] arr { get; set; }
        public string snailedArrayString { get; private set; }

        public SnailMatrix() { }
        public SnailMatrix(int size) { this.arr = new int[size, size]; }
        ~SnailMatrix() { }


        public void fillInSnailWay()
        {
            this.arr = SnailMatrix.snailAlgorithm(this.arr, SnailMatrix.fillMatrix);
        }

        public void printInSnailWay()
        {
            SnailMatrix.snailAlgorithm(this.arr, SnailMatrix.printInSnailWay);
        }

        public void printMatrix()
        {
            SnailMatrix.printMatrix(this.arr);
        }

        public bool writeToDB()
        {
            Console.WriteLine("\n\nWriting to database...");
            if (this.convertArrayToString())
            {
                using (var db = new SnailContext())
                {
                    db.Snails.Add(this);
                    db.SaveChanges();
                    Console.WriteLine("The result was written successfully!");
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Array is empty!");
                return false;
            }
        }





        public delegate int Action(ref int[,] arr, int i, int j, int num);

        public static int[,] snailAlgorithm(int[,] arr, Action func)
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

        public static int fillMatrix(ref int[,] arr, int i, int j, int num)
        {
            arr[i, j] = num;
            num++;
            return num;
        }

        public static int printInSnailWay(ref int[,] arr, int i, int j, int num)
        {
            Console.Write(arr[i, j] + " ");
            return 1;
        }

        public static void printMatrix(int[,] arr)
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



        public static int setValidSize()
        {
            int sizeOfArr;

            while (true)
            {
                Console.Write("Enter a size of matrix: ");

                if (!int.TryParse(Console.ReadLine(), out sizeOfArr) || sizeOfArr <= 0)
                    Console.WriteLine("Input error! Try again.");
                else
                    return sizeOfArr;
            }
        }


        private bool convertArrayToString()
        {
            if (this.arr.Length < 1) return false;

            this.snailedArrayString = "";
            int j = 0;
            for (int i = 1; i <= arr.GetLength(0) / 2; i++)
            {
                for (j = i - 1; j < arr.GetLength(0) - i + 1; j++)
                    this.snailedArrayString += this.arr[i - 1, j] + " ";

                for (j = i; j < arr.GetLength(0) - i + 1; j++)
                    this.snailedArrayString += this.arr[j, arr.GetLength(0) - i] + " ";

                for (j = arr.GetLength(0) - i - 1; j >= i - 1; --j)
                    this.snailedArrayString += this.arr[arr.GetLength(0) - i, j] + " ";

                for (j = arr.GetLength(0) - i - 1; j >= i; j--)
                    this.snailedArrayString += this.arr[j, i - 1] + " ";
            }
            if (arr.GetLength(0) % 2 == 1)
                this.snailedArrayString += this.arr[this.arr.GetLength(0) / 2, this.arr.GetLength(0) / 2] + " ";

            return true;
        }

        public static List<SnailMatrix> readFromDB()
        {
            Console.WriteLine("Reading from database...");
            using (var db = new SnailContext())
            {
                var prevSnails = db.Snails.OrderByDescending(c => c.Id).ToList();
                int counter = 0;

                if (prevSnails.Any(o => o.Id != null))
                {
                    foreach (var elem in prevSnails)
                    {
                        if (counter++ >= 5) break;
                        Console.WriteLine("result #" + elem.Id + ": " + elem.snailedArrayString);
                    }
                }
                else
                {
                    SnailMatrix[] sm = new SnailMatrix[3];
                    for(int i = 0; i < sm.Length; i++){
                        sm[i] = new SnailMatrix();
                        sm[i].snailedArrayString = "test string for first load";
                    }
                    db.Snails.AddRange(sm);
                    db.SaveChanges();

                    Console.WriteLine("Database is empty!");
                }
                Console.WriteLine();
                return prevSnails;
            }
            //return false;//for future
        }
    }
}
