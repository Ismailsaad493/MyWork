using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    public interface ILoanable
    {
        void Borrow(Member member);
        void ReturnBook();
    }
   

}
