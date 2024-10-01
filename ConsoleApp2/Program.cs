using System;
using System.Data;
using System.Data.SqlClient;

namespace BankTransactionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "#####";

            string fromAccount = "1234567890"; 
            string toAccount = "0987654321";  
            decimal transferAmount = 100; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    
                    using (SqlCommand cmd = new SqlCommand("UPDATE Accounts SET Balance = Balance - @Amount WHERE AccountNumber = @AccountNumber", connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Amount", transferAmount);
                        cmd.Parameters.AddWithValue("@AccountNumber", fromAccount);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("error");
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("UPDATE Accounts SET Balance = Balance + @Amount WHERE AccountNumber = @AccountNumber", connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Amount", transferAmount);
                        cmd.Parameters.AddWithValue("@AccountNumber", toAccount);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("error");
                        }
                    }

                    transaction.Commit();
                    Console.WriteLine("S");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }
    }
}
