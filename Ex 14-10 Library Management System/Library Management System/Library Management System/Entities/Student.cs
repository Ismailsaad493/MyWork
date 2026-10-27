namespace Library_Management_System.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Added a Name field
        public string UniversityId { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public ICollection<BookLoan> BookLoans { get; set; } = new List<BookLoan>();
    }
}