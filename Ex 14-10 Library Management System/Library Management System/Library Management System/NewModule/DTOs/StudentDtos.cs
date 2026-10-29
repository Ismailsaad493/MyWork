namespace Library_Management_System.NewModule.Dtos
{
    public class StudentUpdateDto
    {
        public string SName { get; set; } = string.Empty;

        // Links the student to a specific department (e.g., "D1")
        public string DId { get; set; } = string.Empty;
    }
}