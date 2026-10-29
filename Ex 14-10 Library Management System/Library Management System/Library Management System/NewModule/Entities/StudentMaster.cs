using Library_Management_System.Entities;
using System;

namespace Library_Management_System.NewModule.Entities
{
    public class StudentMaster : AuditableEntity
    {
        public string SId { get; set; } = null!; // String PK for S1, S2...
        public string SName { get; set; } = null!;

        // Foreign Key
        public string DId { get; set; } = null!;
        public DepartmentMaster Department { get; set; } = null!;
    }
}