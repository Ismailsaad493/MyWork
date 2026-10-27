using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}