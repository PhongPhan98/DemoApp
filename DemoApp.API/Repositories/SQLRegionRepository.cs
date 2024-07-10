using Azure.Core;
using DemoApp.API.Data;
using DemoApp.API.Models.Domain;
using DemoApp.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly DemoAppDbContext _dbContext;
        public SQLRegionRepository(DemoAppDbContext dbContext)
        {
               this._dbContext = dbContext;
        }

        public async Task<List<RegionDto>> GetAllAsync()
        {
            var regionDomainList = await _dbContext.Regions.ToListAsync();
            return regionDomainList.Select(regionDomain => new RegionDto() {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            }).ToList();
        }


        public async Task<RegionDto> CreateAsync(AddRegionRequestDto request)
        {
            var record = new Region()
            {
                Id = request.Id,
                Name = request.Name,
                Code = request.Code,
                RegionImageUrl = request.RegionImageUrl
            };

             await _dbContext.Regions.AddAsync(record);
            await _dbContext.SaveChangesAsync();
            return new RegionDto()
            {
                Id = record.Id,
                Name = record.Name,
                Code = record.Code,
                RegionImageUrl = record.RegionImageUrl
            };
        }

        public async Task<RegionDto?> DeleteAsync(Guid regionId)
        {
            var record = await _dbContext.Regions.FindAsync(regionId);
            if (record == null) return null;
            _dbContext.Regions.Remove(record);
            await _dbContext.SaveChangesAsync();
            return new RegionDto()
            {
                Id = record.Id,
                Name = record.Name,
                Code = record.Code,
                RegionImageUrl = record.RegionImageUrl
            }; 
        }
        public async Task<RegionDto?> GetAsync(Guid regionId)
        {
            var dataDomain = await _dbContext.Regions.FindAsync(regionId);
            if (dataDomain == null) return null;
            return new RegionDto()
            {
                Id = dataDomain.Id,
                Name = dataDomain.Name,
                Code = dataDomain.Code,
                RegionImageUrl = dataDomain.RegionImageUrl
            };
        }

        public async Task<RegionDto?> UpdateAsync(Guid regionId, UpdateSudentRequestDto updateRegionRequestDto)
        {
            var record = await _dbContext.Regions.FindAsync(regionId);
            if (record == null) return null;
            record.Name = updateRegionRequestDto.Name;
            record.Code = updateRegionRequestDto.Code;
            record.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            _dbContext.Regions.Update(record);
            await _dbContext.SaveChangesAsync();
            return new RegionDto()
            {
                Id = record.Id,
                Name = updateRegionRequestDto.Name,
                Code = updateRegionRequestDto.Code,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };
        }
    }
}
