using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Library_Management_System
{
    public class Library
    {
        public List<Book> Books { get; set; } = new List<Book>();

        private readonly string _connectionString = "Server=localhost;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public void FetchBookFromDatabase()
        {
            Books.Clear();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Title, Author, IsAvailable FROM Books";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Books.Add(new Book
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Author = reader.GetString(2),
                                IsAvailable = reader.GetBoolean(3)
                            });
                        }
                    }
                }
            }
        }

        public void UpdateBookInDatabase(Book book)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                int isAvailableBit = book.IsAvailable ? 1 : 0;
                string query = $"UPDATE Books SET IsAvailable = {isAvailableBit} WHERE Id = {book.Id}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery(); 
                }
            }
        }

        public List<Book> SearchByAuthor(string author)
        {
            return Books.Where(b => b.Author.ToLower().Contains(author.ToLower())).ToList();
        }

        public event Action<Book> OnBookOverdue;

        public async Task CheckoutBookAsync(int bookId, Member member)
        {
            await Task.Delay(1500);
            var book = Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null && book.IsAvailable)
            {
                book.Borrow(member);
                UpdateBookInDatabase(book);
            }
        }

        public async Task CheckForOverdueBooksAsync()
        {
            await Task.Delay(4000);
            var checkedOutBooks = Books.Where(b => !b.IsAvailable && b.DueDate.HasValue);

            foreach (var book in checkedOutBooks)
            {
                if (DateTime.Now > book.DueDate.Value)
                {
                    OnBookOverdue?.Invoke(book);
                }
            }
        }
    }
}