namespace Library_Management_System.Entities
{
    public interface IAuditable
    {
        string? CreatedBy { get; set; }
        string? UpdatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        int UpdateCount { get; set; }
    }
}