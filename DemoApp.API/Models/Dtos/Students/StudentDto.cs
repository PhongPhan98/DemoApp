namespace DemoApp.API.Models.DTO.Students
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int Old { get; set; }
        public string? AvataUrl { get; set; }
    }
}
