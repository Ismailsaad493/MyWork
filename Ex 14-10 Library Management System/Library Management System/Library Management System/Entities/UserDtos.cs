namespace Library_Management_System.Entities
{
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Student";
    }

    public class LoginDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class UpdateUserDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public bool? IsActive { get; set; }
    }
    public class AuthResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }

    public class BorrowHistoryDto
    {
        public int LoanId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}