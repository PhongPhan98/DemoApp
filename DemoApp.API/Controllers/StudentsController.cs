﻿using DemoApp.API.Data;
using DemoApp.API.Models.DTO;
using DemoApp.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DemoAppDbContext dbContext;
        private readonly IStudentRepository studentRepository;

        public StudentController(DemoAppDbContext dbContext, IStudentRepository studentRepository)
        {
            this.dbContext = dbContext;
            this.studentRepository = studentRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var studentsDomain = await studentRepository.GetAllAsync();

            var studentsDto = new List<StudentDto>();

            foreach (var student in studentsDomain)
            {
                studentsDto.Add(new StudentDto()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    PhoneNumber = student.PhoneNumber,
                    Email = student.Email,
                    Old = student.Old,
                    AvataUrl = student.AvataUrl
                });
            }

            return Ok(studentsDto);

        }


        // Get by ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var record = await studentRepository.GetAsync(id);
            if (record == null) return NotFound();
            return Ok(record);

        }

        // Create new record
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddStudentRequestDto request)
        {

            var record = await studentRepository.CreateAsync(request);
            if (record == null) return NotFound();
            return CreatedAtAction(nameof(Create), new { id = record.Id }, record);
        }

        // Update data
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStudentRequestDto updateStudentRequestDto)
        {
            var record = await studentRepository.UpdateAsync(id, updateStudentRequestDto);
            if (record == null) return NotFound();
            return Ok(record);
        }


        // Delete data
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var record = await studentRepository.DeleteAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }
    }
}