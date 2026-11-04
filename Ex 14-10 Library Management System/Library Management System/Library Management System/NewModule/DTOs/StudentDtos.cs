namespace Library_Management_System.NewModule.Dtos
{
    // Used for POST and PUT requests (Input)
    public class StudentUpdateDto
    {
        public string SName { get; set; } = string.Empty;
        public string DId { get; set; } = string.Empty;
    }

    // ADD THIS NEW CLASS: Used for GET requests (Output)
    public class StudentDto
    {
        public string SId { get; set; } = string.Empty;
        public string SName { get; set; } = string.Empty;
        public string DId { get; set; } = string.Empty;
        public string? DepartmentName { get; set; }
    }
}