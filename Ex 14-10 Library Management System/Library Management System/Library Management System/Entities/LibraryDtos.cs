namespace Library_Management_System.Entities
{
    public class IssueBookDto
    {
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public int LoanDurationDays { get; set; } = 14;
    }

    public class BookLoanResponseDto
    {
        public int LoanId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}