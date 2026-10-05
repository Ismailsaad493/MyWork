using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{

    public class Book : ILoanable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; } = true;

        public string CheckedOutBy { get; set; }
        public DateTime? DueDate { get; set; }

        public void Borrow(Member member)
        {
            IsAvailable = false;
            CheckedOutBy = member.name;
            DueDate = DateTime.Now.AddSeconds(3);
        }
        public void ReturnBook()
        {
            IsAvailable = true;
            CheckedOutBy = null;
            DueDate = null;
        }

    }
   
}
