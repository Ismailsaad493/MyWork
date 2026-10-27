using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public string? Bio { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}