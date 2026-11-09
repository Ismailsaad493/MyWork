using System.Text.Json.Serialization;

namespace Library_Management_System.NewModule.Dtos
{
    public class DepartmentCreateDto
    {
        [JsonPropertyName("dName")]
        public string DName { get; set; } = string.Empty;
    }

    public class DepartmentUpdateDto
    {
        [JsonPropertyName("dName")]
        public string DName { get; set; } = string.Empty;
    }
}