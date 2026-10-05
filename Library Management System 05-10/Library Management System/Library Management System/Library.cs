using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library_Management_System
{
    public class Library
    {
        public List<Book> Books { get; set; } = new List<Book>();
        private readonly string _filePath = "Library_data.json";

        public void SaveState()
        {

            string json = JsonSerializer.Serialize(Books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        public void LoadState()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                Books = JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
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
                SaveState();
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
