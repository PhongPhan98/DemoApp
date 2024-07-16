using DemoApp.API.Models;
using DemoApp.API.Models.DTO.Teachers;

namespace DemoApp.API.Interfaces
{
    public interface ITeacherRepository
    {
        public Task<PaginatedList<TeacherDto>> GetAllAsync(int pageIndex, int pageSize);
        public Task<TeacherDto?> GetAsync(Guid regionId);
        public Task<TeacherDto> CreateAsync(AddTeacherRequestDto region);
        public Task<TeacherDto> UpdateAsync(Guid regionId, UpdateTeacherRequestDto region);
        public Task<TeacherDto?> DeleteAsync(Guid regionId);
    }
}
