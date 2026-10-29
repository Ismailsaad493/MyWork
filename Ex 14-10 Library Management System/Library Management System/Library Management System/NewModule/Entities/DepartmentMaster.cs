using Library_Management_System.Entities;
using System;
using System.Collections.Generic;

namespace Library_Management_System.NewModule.Entities
{
    public class DepartmentMaster : AuditableEntity
    {
        public string DId { get; set; } = null!; // String PK for D1, D2...
        public string DName { get; set; } = null!;

        // Navigation Property
        public ICollection<StudentMaster> Students { get; set; } = new List<StudentMaster>();
    }
}