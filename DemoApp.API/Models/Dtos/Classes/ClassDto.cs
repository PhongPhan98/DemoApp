namespace DemoApp.API.Models.DTO.Classes
{
    public class ClassDto
    {
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public Guid TeacherId { get; set; }
    }
}
