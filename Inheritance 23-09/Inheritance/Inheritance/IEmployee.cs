using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    public interface IEmployee
    {
        int EmpId { get; set; }

        string EmpName { get; set; }

        string Department { get; set; }

        void DisplayEmployeeDetails();

        decimal 
    }
}
