using Microsoft.Data.SqlClient;
using MySqlConnector;
using System;

namespace Library_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Book ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Book Name: ");
            string title = Console.ReadLine();

            Console.Write("Enter Author Name: ");
            string author = Console.ReadLine();

            string connectionString = "Server=localhost;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Books (Id, Title, Author, IsAvailable) VALUES (@Id, @Title, @Author, 1)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Author", author);

                    connection.Open();
                    int rowsAdded = command.ExecuteNonQuery(); 
                    if (rowsAdded > 0)
                    {
                        Console.WriteLine("\nSuccess! Book saved to SQL Server database.");
                    }
                }
            }

            Console.ReadLine(); 
        }
    }
}