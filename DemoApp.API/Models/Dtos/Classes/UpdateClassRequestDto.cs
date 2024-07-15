namespace DemoApp.API.Models.DTO.Classes
{
    public class UpdateClassRequestDto
    {
        public string ClassName { get; set; }
        public string Description { get; set; }
        public Guid TeacherId { get; set; }
    }
}
