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

            Console.WriteLine("Connecting to SQL Server...");
            myLibrary.FetchBookFromDatabase();

            myLibrary.OnBookOverdue += (book) =>
            {
                Console.WriteLine($"[OVERDUE ALERT] '{book.Title}' is overdue");
            };

            Member user = new Member { ID = 101, name = "TestUser" };

            Console.WriteLine("Checking out a book...");
            await myLibrary.CheckoutBookAsync(1, user);

            Console.WriteLine("Simulating time passing for overdue check...");
            await myLibrary.CheckForOverdueBooksAsync();

            Console.WriteLine("Process complete! Press Enter to Exit...");
            Console.ReadLine();
        }

        }
    }


  