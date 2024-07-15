namespace DemoApp.API.Models.DTO.Teachers
{
    public class AddTeacherRequestDto
    {
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Age { get; set; }
        public string Phone { get; set; }
    }
}
