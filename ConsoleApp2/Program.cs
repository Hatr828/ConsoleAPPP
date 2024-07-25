using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Random rd = new Random();

            int size = 10;

            int[,] matrix1 = new int[size, size];
            int[,] matrix2 = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix1[i, j] = rd.Next(-100, 100);
                    matrix2[i, j] = rd.Next(-100, 100);
                }
            }

            int[,] result = await MatrixMultiply(matrix1, matrix2);

            PrintMatrix(result);
            Console.ReadLine();
        }

        public async static Task<int[,]> MatrixMultiply(int[,] matrix1, int[,] matrix2)
        {
            int size1 = matrix1.GetLength(0);
            int size2 = matrix2.GetLength(1);
            int size = matrix1.GetLength(1);

            int[,] result = new int[size1, size2];

            await Task.Run(() =>
            {
                for (int i = 0; i < size1; i++)
                {
                    for (int j = 0; j < size2; j++)
                    {
                            result[i,j] = matrix1[i, k] * matrix2[k, j];
                    }
                }
            });

            return result;
        }

        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
