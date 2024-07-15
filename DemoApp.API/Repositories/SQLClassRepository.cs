using AutoMapper;
using DemoApp.API.Data;
using DemoApp.API.Interfaces;
using DemoApp.API.Models.Domain;
using DemoApp.API.Models.DTO.Classes;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.API.Repositories
{
    public class SQLClassRepository : IClassRepository
    {
        private readonly DemoAppDbContext _dbContext;
        private readonly IMapper mapper;
        public SQLClassRepository(DemoAppDbContext dbContext, IMapper mapper)
        {
               this._dbContext = dbContext;
               this.mapper = mapper;
        }

        public async Task<List<ClassDto>> GetAllAsync()
        {
            var classDomainList = await _dbContext.Classes.ToListAsync();
            return mapper.Map<List<ClassDto>>(classDomainList);
           
        }


        public async Task<ClassDto> CreateAsync(AddClassRequestDto request)
        {
            var record = mapper.Map<Class>(request);
            await _dbContext.Classes.AddAsync(record);
            await _dbContext.SaveChangesAsync();
            return mapper.Map<ClassDto>(record);
        }

        public async Task<ClassDto?> DeleteAsync(Guid classID)
        {
            var record = await _dbContext.Classes.FindAsync(classID);
            if (record == null) return null;
            _dbContext.Classes.Remove(record);
            await _dbContext.SaveChangesAsync();
            return mapper.Map<ClassDto>(record);
        }
        public async Task<ClassDto?> GetAsync(Guid classId)
        {
            var dataDomain = await _dbContext.Classes.FindAsync(classId);
            if (dataDomain == null) return null;
            return mapper.Map<ClassDto>(dataDomain);
        }

        public async Task<ClassDto?> UpdateAsync(Guid classId, UpdateClassRequestDto updateClassRequestDto)
        {
            var record = await _dbContext.Classes.FindAsync(classId);
            if (record == null) return null;
            record.ClassName = updateClassRequestDto.ClassName;
            record.Description = updateClassRequestDto.Description;
            record.TeacherId = updateClassRequestDto.TeacherId;
            _dbContext.Classes.Update(record);
            await _dbContext.SaveChangesAsync();
            return mapper.Map<ClassDto>(record);
        }
    }
}
