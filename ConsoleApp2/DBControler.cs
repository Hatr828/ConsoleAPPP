using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class DBControler
    {
        public static void ShowAllNames(SqlConnection connection)
        {
            string query = "SELECT Name FROM VegetablesFruits";
            SqlCommand command = new SqlCommand(query, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader["Name"].ToString());
                }
            }
        }

        public static void ShowAllCalors(SqlConnection connection)
        {
            string query = "SELECT DISTINCT Color FROM VegetablesFruits";
            SqlCommand command = new SqlCommand(query, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader["Color"].ToString());
                }
            }
        }

        public static void ShowMinColories(SqlConnection connection)
        {
            string query = "SELECT MIN(Calories) FROM VegetablesFruits";
            SqlCommand command = new SqlCommand(query, connection);

            var result = command.ExecuteScalar();
            Console.WriteLine($"min: {result}");
        }

        public static void ShowAvgCalories(SqlConnection connection)
        {
            string query = "SELECT AVG(Calories) FROM VegetablesFruits";
            SqlCommand command = new SqlCommand(query, connection);

            var result = command.ExecuteScalar();
            Console.WriteLine($"avg: {result}");
        }

        public static void ShowVegetablesCount(SqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM VegetablesFruits WHERE Type = 'Овощ'";
            SqlCommand command = new SqlCommand(query, connection);

            var result = command.ExecuteScalar();
            Console.WriteLine($"num: {result}");
        }

        public static void ShowFruitsCount(SqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM VegetablesFruits WHERE Type = 'Фрукт'";
            SqlCommand command = new SqlCommand(query, connection);

            var result = command.ExecuteScalar();
            Console.WriteLine($"num: {result}");
        }

        public static void ShowCountByColor(SqlConnection connection, string color)
        {
            string query = "SELECT COUNT(*) FROM VegetablesFruits WHERE Color = @color";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@color", color);

            var result = command.ExecuteScalar();
            Console.WriteLine($"nums & color {color}: {result}");
        }

        static void ShowCountByEachColor(SqlConnection connection)
        {
            string query = "SELECT Color, COUNT(*) as Count FROM VegetablesFruits GROUP BY Color";
            SqlCommand command = new SqlCommand(query, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Color: {reader["Color"]}, num: {reader["Count"]}");
                }
            }
        }

        public static void ShowBelowCalories(SqlConnection connection, int maxCalories)
        {
            string query = "SELECT * FROM VegetablesFruits WHERE Calories < @maxCalories";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@maxCalories", maxCalories);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]} | {reader["Calories"]}");
                }
            }
        }

        public static void ShowAboveCalories(SqlConnection connection, int minCalories)
        {
            string query = "SELECT * FROM VegetablesFruits WHERE Calories > @minCalories";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@minCalories", minCalories);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]} | {reader["Calories"]}");
                }
            }
        }

        public static void ShowInCaloriesRange(SqlConnection connection, int minCalories, int maxCalories)
        {
            string query = "SELECT * FROM VegetablesFruits WHERE Calories BETWEEN @minCalories AND @maxCalories";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@minCalories", minCalories);
            command.Parameters.AddWithValue("@maxCalories", maxCalories);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]} | {reader["Calories"]}");
                }
            }
        }

        public static void ShowYellowOrRed(SqlConnection connection)
        {
            string query = "SELECT * FROM VegetablesFruits WHERE Color = 'Желтый' OR Color = 'Красный'";
            SqlCommand command = new SqlCommand(query, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]} | {reader["Color"]}");
                }
            }
        }
    }
}
