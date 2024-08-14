using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            string input = Console.ReadLine();

            try
            {
                double result = Test(input);
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"error");
            }
        }

    }

    static double Test(string expression)
    {
        var dataTable = new System.Data.DataTable();
        var result = dataTable.Compute(expression, string.Empty);
        return Convert.ToDouble(result);
    }
}
