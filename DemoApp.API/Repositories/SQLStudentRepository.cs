using Azure.Core;
using DemoApp.API.Data;
using DemoApp.API.Models.Domain;
using DemoApp.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.API.Repositories
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly DemoAppDbContext _dbContext;
        public SQLStudentRepository(DemoAppDbContext dbContext)
        {
               this._dbContext = dbContext;
        }

        public async Task<List<StudentDto>> GetAllAsync()
        {
            var studentDomainList = await _dbContext.Students.ToListAsync();
            return studentDomainList.Select(studentDomain => new StudentDto() {
                Id = studentDomain.Id,
                FirstName = studentDomain.FirstName,
                LastName = studentDomain.LastName,
                PhoneNumber = studentDomain.PhoneNumber,
                Email = studentDomain.Email,
                Old = studentDomain.Old,
                AvataUrl = studentDomain.AvataUrl
            }).ToList();
        }


        public async Task<StudentDto> CreateAsync(AddStudentRequestDto request)
        {
            var record = new Student()
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Old = request.Old,
                AvataUrl = request.AvataUrl
            };

            await _dbContext.Students.AddAsync(record);
            await _dbContext.SaveChangesAsync();
            return new StudentDto()
            {
                Id = record.Id,
                FirstName = record.FirstName,
                LastName = record.LastName,
                PhoneNumber = record.PhoneNumber,
                Email = record.Email,
                Old = record.Old,
                AvataUrl = record.AvataUrl
            };
        }

        public async Task<StudentDto?> DeleteAsync(Guid StudentId)
        {
            var record = await _dbContext.Students.FindAsync(StudentId);
            if (record == null) return null;
            _dbContext.Students.Remove(record);
            await _dbContext.SaveChangesAsync();
            return new StudentDto()
            {
                Id = record.Id,
                FirstName = record.FirstName,
                LastName = record.LastName,
                PhoneNumber = record.PhoneNumber,
                Email = record.Email,
                Old = record.Old,
                AvataUrl = record.AvataUrl
            }; 
        }
        public async Task<StudentDto?> GetAsync(Guid StudentId)
        {
            var dataDomain = await _dbContext.Students.FindAsync(StudentId);
            if (dataDomain == null) return null;
            return new StudentDto()
            {
                Id = dataDomain.Id,
                FirstName = dataDomain.FirstName,
                LastName = dataDomain.LastName,
                PhoneNumber = dataDomain.PhoneNumber,
                Email = dataDomain.Email,
                Old = dataDomain.Old,
                AvataUrl = dataDomain.AvataUrl
            };
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
            return new StudentDto()
            {
                Id = updateStudentRequestDto.Id,
                FirstName = updateStudentRequestDto.FirstName,
                LastName = updateStudentRequestDto.LastName,
                PhoneNumber = updateStudentRequestDto.PhoneNumber,
                Email = updateStudentRequestDto.Email,
                Old = updateStudentRequestDto.Old,
                AvataUrl = updateStudentRequestDto.AvataUrl
            };
        }
    }
}
