using DemoApp.API.Models.Domain;
using DemoApp.API.Models.DTO;

namespace DemoApp.API.Repositories
{
    public interface IRegionRepository
    {
        public Task<List<RegionDto>> GetAllAsync();
        public Task<RegionDto?> GetAsync(Guid regionId);
        public Task<RegionDto> CreateAsync(AddRegionRequestDto region);
        public Task<RegionDto> UpdateAsync(Guid regionId, UpdateSudentRequestDto region);
        public Task<RegionDto?> DeleteAsync(Guid regionId);
    }
}
