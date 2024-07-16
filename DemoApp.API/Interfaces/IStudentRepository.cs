using DemoApp.API.Models;
using DemoApp.API.Models.DTO.Students;

namespace DemoApp.API.Interfaces
{
    public interface IStudentRepository
    {
        public Task<PaginatedList<StudentDto>> GetAllAsync(int pageIndex, int pageSize);
        public Task<StudentDto?> GetAsync(Guid regionId);
        public Task<StudentDto> CreateAsync(AddStudentRequestDto region);
        public Task<StudentDto> UpdateAsync(Guid regionId, UpdateStudentRequestDto region);
        public Task<StudentDto?> DeleteAsync(Guid regionId);
    }
}
