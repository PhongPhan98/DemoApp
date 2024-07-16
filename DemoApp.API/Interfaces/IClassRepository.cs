using DemoApp.API.Models;
using DemoApp.API.Models.DTO.Classes;

namespace DemoApp.API.Interfaces
{
    public interface IClassRepository
    {
        public Task<PaginatedList<ClassDto>> GetAllAsync(int pageIndex, int pageSize);
        public Task<ClassDto?> GetAsync(Guid regionId);
        public Task<ClassDto> CreateAsync(AddClassRequestDto region);
        public Task<ClassDto> UpdateAsync(Guid regionId, UpdateClassRequestDto region);
        public Task<ClassDto?> DeleteAsync(Guid regionId);
    }
}
