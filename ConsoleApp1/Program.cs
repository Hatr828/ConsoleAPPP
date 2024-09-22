using System;
using System.Data.SqlClient;

class Program
{
    private static string connectionString = "#########################";

    static void Main()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                connection.Close();
                Console.WriteLine("Connected");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"ex.Message");
        }
    }
  
}
