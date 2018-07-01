using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LaplaceMethod
{
    class LaplaceParallel
    {
        public double LeftBorder { get; set; }
        public double RightBorder { get; set; }
        public double TopBorder { get; set; }
        public double BottomBorder { get; set; }
        
        public double Step { get; set; }

        public int[] XInterval { get; set; }
        public int[] YInterval { get; set; }

        public int Size { get; set; }

        public Point[,] Matrix { get; set; }
        public Point[,] DuplicatedMatrix { get; set; }

        public LaplaceParallel(
            double leftBorder,
            double topBorder, 
            double rightBorder, 
            double bottomBorder, 
            int[] xInterval, 
            int[] yInterval, 
            double step
        )
        {
            Step = step;
            XInterval = xInterval;
            YInterval = yInterval;
            Size = (int)(Math.Abs(XInterval[0]) + Math.Abs(YInterval[1]) / Step);
            LeftBorder = leftBorder;
            TopBorder = topBorder;
            RightBorder = rightBorder;
            BottomBorder = bottomBorder;

            Matrix = new Point[Size + 1, Size + 1];
            DuplicatedMatrix = new Point[Size + 1, Size + 1];

            FillGrid(Matrix);
            FillGrid(DuplicatedMatrix);
            Run();
        }

        public void FillGrid(Point[,] matrix)
        {
            double xValue = XInterval[0];
            double yValue = YInterval[1];
            double zValue = 0;

            for (int i = 0; i <= Size; i++)
            {
                for (int j = 0; j <= Size; j++)
                {
                    zValue = 0;
                    if (i == 0)
                    {
                        zValue = TopBorder;
                    }
                    else if (i == Size)
                    {
                        zValue = BottomBorder;
                    }
                    else if (j == 0)
                    {
                        zValue = LeftBorder;
                    }
                    else if (j == Size)
                    {
                        zValue = RightBorder;
                    }
                    matrix[i, j] = new Point(xValue, yValue, zValue);
                    xValue = Math.Round(xValue + Step, 1);
                }
                xValue = 0;
                yValue = Math.Round(yValue - Step, 1);
            }
        }

        public void Run()
        {
            Thread firstMatrixThread = new Thread(CalcFirstMatrix);
            Thread secondMatrixThread = new Thread(CalcSecondMatrix);

            firstMatrixThread.Start();
            secondMatrixThread.Start();
            firstMatrixThread.Join();
            secondMatrixThread.Join();

            AddMatrixes();

            CalcFinalMatrix();
        }

        public void CalcFirstMatrix()
        {
            CalcInitialApproximation();
        }

        public void CalcSecondMatrix()
        {
            CalcInitialReverseApproximation();
        }

        public void AddMatrixes()
        {
            for (var i = 1; i < Size; i++)
            {
                for (var j = 1; j < Size; j++)
                {
                    Matrix[i, j].Z = (Matrix[i, j].Z + DuplicatedMatrix[i, j].Z) / 2;
                }
            }
        }

        public void CalcFinalMatrix()
        {
            for (var i = 1; i < Size; i++)
            {
                for (var j = 1; j < Size; j++)
                {
                    Matrix[i, j].Z = (Matrix[i, j - 1].Z + Matrix[i, j + 1].Z) / 2;
                }
            }
        }

        public void CalcInitialApproximation()
        {
            for (int i = 1; i < Size; i++)
            {
                for (int j = 1; j < Size; j++)
                {
                    Matrix[i, j].Z = (Matrix[i - 1, j - 1].Z + Matrix[i - 1, j].Z + Matrix[i, j - 1].Z) / 3;
                }
            }
        }

        public void CalcInitialReverseApproximation()
        {
            for (int i = Size - 1; i >= 1; i--)
            {
                for (int j = Size - 1; j >= 1; j--)
                {
                    DuplicatedMatrix[i, j].Z = (DuplicatedMatrix[i + 1, j + 1].Z + DuplicatedMatrix[i + 1, j].Z + DuplicatedMatrix[i, j + 1].Z) / 3;
                }
            }
        }
    }
}
