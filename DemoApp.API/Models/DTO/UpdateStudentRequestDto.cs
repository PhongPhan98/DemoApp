namespace DemoApp.API.Models.DTO
{
    public class UpdateStudentRequestDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int Old { get; set; }
        public string? AvataUrl { get; set; }
    }
}
