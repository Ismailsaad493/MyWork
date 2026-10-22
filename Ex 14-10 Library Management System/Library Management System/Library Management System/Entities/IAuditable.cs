namespace Library_Management_System.Entities
{
    public interface IAuditable
    {
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}