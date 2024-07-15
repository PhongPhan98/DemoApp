using AutoMapper;
using DemoApp.API.Data;
using DemoApp.API.Interfaces;
using DemoApp.API.Models.DTO.Classes;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly DemoAppDbContext dbContext;
        private readonly IClassRepository classRepository;
        private readonly IMapper mapper;

        public ClassController(DemoAppDbContext dbContext, IClassRepository classRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.classRepository = classRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var studentsDto = await classRepository.GetAllAsync();
            return Ok(studentsDto);

        }

        // Get by ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var record = await classRepository.GetAsync(id);
            if (record == null) return NotFound();
            return Ok(record);

        }

        // Create new record
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddClassRequestDto request)
        {

            var record = await classRepository.CreateAsync(request);
            if (record == null) return NotFound();
            return CreatedAtAction(nameof(Create), new { id = record.ClassId }, record);
        }

        // Update data
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateClassRequestDto updateClassRequestDto)
        {
            var record = await classRepository.UpdateAsync(id, updateClassRequestDto);
            if (record == null) return NotFound();
            return Ok(record);
        }


        // Delete data
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var record = await classRepository.DeleteAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }
    }
}
