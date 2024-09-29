using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "#######";

        string objectName = "aaaa";
        int objectQuantity = 10;
        string objectStatus = "bbbb";

        int newObjectId;

        AddInventoryItem(connectionString, objectName, objectQuantity, objectStatus, out newObjectId);

        Console.WriteLine($"ID: {newObjectId}");
    }

    static void AddInventoryItem(string connectionString, string objectName, int objectQuantity, string objectStatus, out int objectId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("AddInventoryItem", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ObjectName", objectName);
                command.Parameters.AddWithValue("@ObjectQuantity", objectQuantity);
                command.Parameters.AddWithValue("@ObjectStatus", objectStatus);

                SqlParameter outputIdParam = new SqlParameter("@ObjectID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                command.ExecuteNonQuery();

                objectId = (int)outputIdParam.Value;
            }
        }
    }
}
