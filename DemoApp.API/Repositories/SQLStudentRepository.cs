using AutoMapper;
using DemoApp.API.Data;
using DemoApp.API.Interfaces;
using DemoApp.API.Models;
using DemoApp.API.Models.Domain;
using DemoApp.API.Models.DTO.Students;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DemoApp.API.Repositories
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly DemoAppDbContext _dbContext;
        private readonly IMapper mapper;
        public SQLStudentRepository(DemoAppDbContext dbContext, IMapper mapper)
        {
               this._dbContext = dbContext;
               this.mapper = mapper;
        }

        public async Task<PaginatedList<StudentDto>> GetAllAsync(int pageIndex, int pageSize)
        {
            var students = await _dbContext.Students
                .OrderBy(student => student.FirstName)
                .Skip((pageIndex - 1)* pageSize)
                .Take(pageSize)
                .ToListAsync();


            var count = await _dbContext.Students.CountAsync();

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var result = mapper.Map<List<StudentDto>>(students);

             return new PaginatedList<StudentDto>(result, pageIndex, totalPages);
           
        }


        public async Task<StudentDto> CreateAsync(AddStudentRequestDto request)
        {
            var record = mapper.Map<Student>(request);
            await _dbContext.Students.AddAsync(record);
            await _dbContext.SaveChangesAsync();
            return mapper.Map<StudentDto>(record);
        }

        public async Task<StudentDto?> DeleteAsync(Guid StudentId)
        {
            var record = await _dbContext.Students.FindAsync(StudentId);
            if (record == null) return null;
            _dbContext.Students.Remove(record);
            await _dbContext.SaveChangesAsync();
            return mapper.Map<StudentDto>(record);
        }
        public async Task<StudentDto?> GetAsync(Guid StudentId)
        {
            var dataDomain = await _dbContext.Students.FindAsync(StudentId);
            if (dataDomain == null) return null;
            return mapper.Map<StudentDto>(dataDomain);
        }

        public async Task<StudentDto?> UpdateAsync(Guid studentId, UpdateStudentRequestDto updateStudentRequestDto)
        {
            var record = await _dbContext.Students.FindAsync(studentId);
            if (record == null) return null;
            record.FirstName = updateStudentRequestDto.FirstName;
            record.LastName = updateStudentRequestDto.LastName;
            record.PhoneNumber = updateStudentRequestDto.PhoneNumber;
            record.Email = updateStudentRequestDto.Email;
            record.Old = updateStudentRequestDto.Old;
            record.AvataUrl = updateStudentRequestDto.AvataUrl;

            _dbContext.Students.Update(record);
            await _dbContext.SaveChangesAsync();
            return mapper.Map<StudentDto>(record);
        }
    }
}
