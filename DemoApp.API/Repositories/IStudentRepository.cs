using DemoApp.API.Models.Domain;
using DemoApp.API.Models.DTO;

namespace DemoApp.API.Repositories
{
    public interface IStudentRepository
    {
        public Task<List<StudentDto>> GetAllAsync();
        public Task<StudentDto?> GetAsync(Guid regionId);
        public Task<StudentDto> CreateAsync(AddStudentRequestDto region);
        public Task<StudentDto> UpdateAsync(Guid regionId, UpdateStudentRequestDto region);
        public Task<StudentDto?> DeleteAsync(Guid regionId);
    }
}
