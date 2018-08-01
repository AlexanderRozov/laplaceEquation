using System;
using System.Collections.Generic;
using System.IO;
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
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = customCulture;

            LaplaceParallel warmup = new LaplaceParallel(0, 1, 0, -3, new int[] { 0, 1 }, new int[] { 0, 1 }, 0.1, 100);

            LaplaceParallel laplace1 = new LaplaceParallel(0, 1, 0, -3, new int[] { 0, 1 }, new int[] { 0, 1 }, 0.1, 100);
            LaplaceParallel laplace2 = new LaplaceParallel(0, 1, 0, -3, new int[] { 0, 1 }, new int[] { 0, 1 }, 0.01, 100);
            LaplaceParallel laplace3 = new LaplaceParallel(0, 1, 0, -3, new int[] { 0, 1 }, new int[] { 0, 1 }, 0.001, 100);

            Console.WriteLine("Среднее время выполнения программы при ста итерациях на сетке 10*10*10: {0} мс", laplace1.TotalTime);
            Console.WriteLine("Среднее время выполнения программы при ста итерациях на сетке 100*100*100: {0} мс", laplace2.TotalTime);
            Console.WriteLine("Среднее время выполнения программы при ста итерациях на сетке 1000*1000*1000: {0} мс", laplace3.TotalTime);

            WritePointsToFile(laplace2.Matrix);

            Console.ReadLine();
        }

        static void WritePointsToFile(Point[,] matrix)
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, "laplace1.dat");

            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Default))
            {
                sw.WriteLine("# X  Y  Z");
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        sw.WriteLine("  {0}  {1}  {2}", matrix[i, j].X, matrix[i, j].Y, matrix[i, j].Z);
                    }
                    sw.WriteLine();
                }
            }
            Console.WriteLine("Запись в файл завершена");
        }
    }
}
