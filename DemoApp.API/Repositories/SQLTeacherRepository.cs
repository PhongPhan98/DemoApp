using AutoMapper;
using DemoApp.API.Data;
using DemoApp.API.Interfaces;
using DemoApp.API.Models.Domain;
using DemoApp.API.Models.DTO.Teachers;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.API.Repositories
{
    public class SQLTeacherRepository : ITeacherRepository
    {
        private readonly DemoAppDbContext _dbContext;
        private readonly IMapper mapper;
        public SQLTeacherRepository(DemoAppDbContext dbContext, IMapper mapper)
        {
               this._dbContext = dbContext;
               this.mapper = mapper;
        }

        public async Task<List<TeacherDto>> GetAllAsync()
        {
            var teacherDomainList = await _dbContext.Teachers.ToListAsync();
            return mapper.Map<List<TeacherDto>>(teacherDomainList);
           
        }

        public async Task<TeacherDto> CreateAsync(AddTeacherRequestDto request)
        {
            var record = mapper.Map<Teacher>(request);
            await _dbContext.Teachers.AddAsync(record);
            await _dbContext.SaveChangesAsync();
            return mapper.Map<TeacherDto>(record);
        }

        public async Task<TeacherDto?> DeleteAsync(Guid teacherId)
        {
            var record = await _dbContext.Teachers.FindAsync(teacherId);
            if (record == null) return null;
            _dbContext.Teachers.Remove(record);
            await _dbContext.SaveChangesAsync();
            return mapper.Map<TeacherDto>(record);
        }
        public async Task<TeacherDto?> GetAsync(Guid teacherId)
        {
            var dataDomain = await _dbContext.Teachers.FindAsync();
            if (dataDomain == null) return null;
            return mapper.Map<TeacherDto>(dataDomain);
        }

        public async Task<TeacherDto> UpdateAsync(Guid teacherId, UpdateTeacherRequestDto updateTeacherRequestDto)
        {
            var record = await _dbContext.Teachers.FindAsync(teacherId);

            if (record == null) return null;

            record.TeacherName = updateTeacherRequestDto.TeacherName;
            record.Phone = updateTeacherRequestDto.Phone;
            record.Age = updateTeacherRequestDto.Age;

            _dbContext.Teachers.Update(record);
            await _dbContext.SaveChangesAsync();
            return mapper.Map<TeacherDto>(record);
        }
    }
}
