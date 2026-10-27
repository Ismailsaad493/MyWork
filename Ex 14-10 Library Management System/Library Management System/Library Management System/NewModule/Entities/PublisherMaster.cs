namespace Library_Management_System.NewModule.Entities
{
    public class PublisherMaster
    {
        public int PId { get; set; }
        public string PName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<BookMaster>? Books { get; set; }
    }
}