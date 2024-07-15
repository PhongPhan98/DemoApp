namespace DemoApp.API.Models.Domain
{
    public class Student
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }    
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int Old {  get; set; }
        public string? AvataUrl { get; set; }
        public Guid ClassId { get; set; }

    }
}
