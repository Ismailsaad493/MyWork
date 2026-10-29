using System;

namespace Library_Management_System.Entities
{
    public interface IAuditable
    {
        DateTime CreatedAt { get; set; }
        string? CreatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }
        string? UpdatedBy { get; set; }
        int UpdateCount { get; set; }
        bool IsActive { get; set; }
    }
}