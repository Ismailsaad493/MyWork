using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserCode { get; private set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role? Role { get; set; }
        public string UniversityId { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public ICollection<BookLoan> BookLoans { get; set; } = new List<BookLoan>();
    }
}