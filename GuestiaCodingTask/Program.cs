using GuestiaCodingTask.Data;
using GuestiaCodingTask.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;

namespace GuestiaCodingTask
{
    class Program
    {
        static void Main(string[] args)
        {
            DbInitialiser.CreateDb();

            string connectionString = @"Server=.\SQLEXPRESS;Database=GuestiaDB;Trusted_Connection=True;";

            Console.WriteLine("Standard Unregistered Guests:");
            Console.WriteLine("Id|LastName|FirstName|RegistrationDate|GuestGroupId");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                SqlCommand command = new SqlCommand("SELECT * FROM Guests WHERE RegistrationDate IS NULL AND GuestGroupId = 1 ORDER BY LastName ASC;", connection);
               
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string secondName = reader.GetString(2);
                    string firstName = reader.GetString(1).Substring(0, 1);
                    string registration = reader.IsDBNull(3) ? "null" : reader.GetDateTime(3).ToString("yyyy-MM-dd");
                    int guestGroup = reader.GetInt32(4);
                    Console.WriteLine("{0}, {2}, {1}, {3}, {4}", id, firstName, secondName, registration, guestGroup);
                }
                
                reader.Close();
            }

            Console.WriteLine("\n");

            Console.WriteLine("VIP Unregistered Guests:");
            Console.WriteLine("Id|LastName|FirstName|RegistrationDate|GuestGroupId");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Guests WHERE RegistrationDate IS NULL AND GuestGroupId = 2 ORDER BY LastName ASC;", connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string secondName = reader.GetString(2).ToUpper();
                    string firstName = reader.GetString(1);
                    string registration = reader.IsDBNull(3) ? "null" : reader.GetDateTime(3).ToString("yyyy-MM-dd");
                    int guestGroup = reader.GetInt32(4);
                    Console.WriteLine("{0}, {2}, {1}, {3}, {4}", id, firstName, secondName, registration, guestGroup);
                }

                reader.Close();
            }
        }
    }
}
