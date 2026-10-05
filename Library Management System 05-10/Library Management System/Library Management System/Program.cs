using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    internal class program
    {
        static async Task Main(string[] args)
        {
            Library myLibrary = new Library();
            myLibrary.LoadState();

            if (!myLibrary.Books.Any())
            {
                myLibrary.Books.Add(new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" });
                myLibrary.Books.Add(new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee" });
                myLibrary.Books.Add(new Book { Id = 3, Title = "1984", Author = "George Orwell" });
                myLibrary.Books.Add(new Book { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen" });
                myLibrary.Books.Add(new Book { Id = 5, Title = "The Catcher in the Rye", Author = "J.D. Salinger" });
                myLibrary.SaveState();

            }

                myLibrary.OnBookOverdue += (book) =>
                {
                    Console.WriteLine($"[OVERDUE ALERT] '{book.Title}' is overdue");
                };

            Member user = new Member { ID = 101, name = "TestUser" };

                await myLibrary.CheckoutBookAsync(1, user);
                await myLibrary.CheckForOverdueBooksAsync();
            }

        }
    }


  