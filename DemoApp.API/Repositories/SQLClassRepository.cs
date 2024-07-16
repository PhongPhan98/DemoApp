using AutoMapper;
using DemoApp.API.Data;
using DemoApp.API.Interfaces;
using DemoApp.API.Models;
using DemoApp.API.Models.Domain;
using DemoApp.API.Models.DTO.Classes;
using DemoApp.API.Models.DTO.Students;
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

        public async Task<PaginatedList<ClassDto>> GetAllAsync(int pageIndex, int pageSize)
        {
            var classes = await _dbContext.Classes
                .OrderBy(Class => Class.ClassName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            var count = await _dbContext.Classes.CountAsync();

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var result = mapper.Map<List<ClassDto>>(classes);

            return new PaginatedList<ClassDto>(result, pageIndex, totalPages);

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
