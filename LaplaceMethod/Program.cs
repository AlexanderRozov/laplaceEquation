using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LaplaceMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            LaplaceParallel laplace = new LaplaceParallel(0, 1, 0, -3, new int[] { 0, 1 }, new int[] { 0, 1 }, 0.01);
            for (int i = 0; i < laplace.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < laplace.Matrix.GetLength(1); j++)
                {
                    if (laplace.Matrix[i, j] == null)
                    {
                        laplace.Matrix[i, j] = new Point(0, 0, 0);
                    }
                    Console.Write("({0};{1};{2})", laplace.Matrix[i, j].X, laplace.Matrix[i, j].Y, laplace.Matrix[i, j].Z);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
