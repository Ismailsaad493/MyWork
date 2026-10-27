namespace Library_Management_System.NewModule.Entities
{
    public class CategoryMaster
    {
        public int CId { get; set; }
        public string CName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<BookMaster>? Books { get; set; }
    }
}