using AutoMapper;
using DemoApp.API.Data;
using DemoApp.API.Interfaces;
using DemoApp.API.Models.DTO.Teachers;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly DemoAppDbContext dbContext;
        private readonly ITeacherRepository teacherRepository;
        private readonly IMapper mapper;

        public TeacherController(DemoAppDbContext dbContext, ITeacherRepository teacherRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.teacherRepository = teacherRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var studentsDto = await teacherRepository.GetAllAsync();
            return Ok(studentsDto);

        }

        // Get by ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var record = await teacherRepository.GetAsync(id);
            if (record == null) return NotFound();
            return Ok(record);

        }

        // Create new record
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddTeacherRequestDto request)
        {
            var record = await teacherRepository.CreateAsync(request);
            if (record == null) return NotFound();
            return CreatedAtAction(nameof(Create), new { id = record.TeacherId }, record);
        }

        // Update data
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTeacherRequestDto updateTeacherRequestDto)
        {
            var record = await teacherRepository.UpdateAsync(id, updateTeacherRequestDto);
            if (record == null) return NotFound();
            return Ok(record);
        }


        // Delete data
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var record = await teacherRepository.DeleteAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }
    }
}
