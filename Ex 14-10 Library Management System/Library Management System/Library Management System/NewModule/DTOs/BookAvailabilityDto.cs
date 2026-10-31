namespace Library_Management_System.NewModule.Dtos
{
    public class BookAvailabilityDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public int IssuedCopies { get; set; }
    }
}