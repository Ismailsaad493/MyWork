namespace Medicine_Management_API.Models
{
    public class MedicineRequestDto
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
    }

    public class MedicineResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
    }
}
