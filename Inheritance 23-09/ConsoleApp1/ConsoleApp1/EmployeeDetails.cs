using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    public class EmployeeDetails : IEmployee
    {
        public int EmpId { get; set; }

        public string EmpName { get; set; }

        public string Department { get; set; }

        public decimal Monthlysalary { get; set; }

        public EmployeeDetails(int empId, string empName, string department, decimal salary)
        {
            this.EmpId = empId;
            this.EmpName = empName;
            this.Department = department;
            this.Monthlysalary = salary;
        }
        public void DisplayEmployeeDetails()
        {
            Console.WriteLine($"EmpId: {EmpId}, EmpName: {EmpName}, Department: {Department}, Monthlysalary: {Monthlysalary}");
        }

        public decimal CalculateSalary()
        {
            return Monthlysalary * 12;
        }
    }
}