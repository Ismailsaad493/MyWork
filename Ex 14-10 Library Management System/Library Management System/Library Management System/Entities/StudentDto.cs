namespace Library_Management_System.Entities
{
    // Renamed to match what your Service and Controller are looking for
    public class StudentDto
    {
        public string UniversityId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string BloodGroup { get; set; } = string.Empty;
    }

    public class UpdateStudentDto
    {
        public string? Name { get; set; }
        public string? Department { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }
    }

    public class StudentResponseDto
    {
        public int Id { get; set; }
        public string UniversityId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string BloodGroup { get; set; } = string.Empty;
    }
}