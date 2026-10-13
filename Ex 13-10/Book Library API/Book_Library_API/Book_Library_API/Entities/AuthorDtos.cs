namespace Book_Library_API.Entities
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class AddAuthorDto
    {
        public string Name { get; set; } = string.Empty;
    }
    public class UpdateAuthorDto
    {
        public string Name { get; set; } = string.Empty;
    }
}