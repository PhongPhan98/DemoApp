using AutoMapper;
using DemoApp.API.Data;
using DemoApp.API.Interfaces;
using DemoApp.API.Models;
using DemoApp.API.Models.DTO.Students;
using DemoApp.API.Models.DTO.Teachers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Text.Json;

namespace DemoApp.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class TeachersController : ODataController
    {
        private readonly DemoAppDbContext dbContext;
        private readonly ITeacherRepository teacherRepository;
        private readonly IMapper mapper;
        private readonly ILogger<TeachersController> logger;

        public TeachersController(DemoAppDbContext dbContext, ITeacherRepository teacherRepository, IMapper mapper, ILogger<TeachersController> logger)
        {
            this.dbContext = dbContext;
            this.teacherRepository = teacherRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [EnableQuery]
        public async Task<ApiResponse> GetAllV1(int pageIndex = 1, int pageSize = 10)
        {
            logger.LogInformation($"TeachersController >> GetAllV1 >>  pageIndex :{pageIndex},  pageSize: {pageSize}");
            var teachers = await teacherRepository.GetAllAsync(pageIndex, pageSize);
            logger.LogInformation($"TeachersController >> GetAllV1 >>  Finnished get all of teachers: {JsonSerializer.Serialize(teachers)}");
            return new ApiResponse(true, string.Empty, teachers);
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        [EnableQuery]
        public async Task<ApiResponse> GetAllV2(int pageIndex = 1, int pageSize = 10)
        {
            logger.LogInformation($"TeachersController >> GetAllV1 >>  pageIndex :{pageIndex},  pageSize: {pageSize}");
            var teachers = await teacherRepository.GetAllAsync(pageIndex, pageSize);
            logger.LogInformation($"TeachersController >> GetAllV2 >>  Finnished get all of teachers: {JsonSerializer.Serialize(teachers)}");
            return new ApiResponse(true, string.Empty, teachers);
        }

        // Get by ID
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIDV1([FromRoute] Guid id)
        {
            logger.LogInformation($"TeachersController >> GetByIDV1 >> ID: {id}");
            var record = await teacherRepository.GetAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // Get by ID
        [MapToApiVersion("2.0")]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIDV2([FromRoute] Guid id)
        {
            logger.LogInformation($"TeachersController >> GetByIDV2 >> ID: {id}");
            var record = await teacherRepository.GetAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // Create new record
        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> CreateV1([FromBody] AddTeacherRequestDto request)
        {
            logger.LogInformation($"TeachersController >> CreateV1 >> AddTeacherRequestDto:  {JsonSerializer.Serialize(request)}");
            if (!ValidateCreateAsync(request))
            {
                return BadRequest();
            }
            var record = await teacherRepository.CreateAsync(request);
            if (record == null) return NotFound();
            return CreatedAtAction(nameof(CreateV1), new { id = record.TeacherId }, record);
        }

        // Create new record
        [MapToApiVersion("2.0")]
        [HttpPost]
        public async Task<IActionResult> CreateV2([FromBody] AddTeacherRequestDto request)
        {
            logger.LogInformation($"TeachersController >> CreateV2 >> AddTeacherRequestDto:  {JsonSerializer.Serialize(request)}");
            if (!ValidateCreateAsync(request))
            {
                return BadRequest();
            }
            var record = await teacherRepository.CreateAsync(request);
            if (record == null) return NotFound();
            return CreatedAtAction(nameof(CreateV2), new { id = record.TeacherId }, record);
        }

        // Update data
        [MapToApiVersion("1.0")]
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateV1([FromRoute] Guid id, [FromBody] UpdateTeacherRequestDto updateTeacherRequestDto)
        {
            logger.LogInformation($"TeachersController >> UpdateV1 >> UpdateTeacherRequestDto:  {JsonSerializer.Serialize(updateTeacherRequestDto)}; ID: {id}");
            if (!ValidateUpdateAsync(updateTeacherRequestDto))
            {
                return BadRequest();
            }
            var record = await teacherRepository.UpdateAsync(id, updateTeacherRequestDto);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // Update data
        [MapToApiVersion("2.0")]
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateV2([FromRoute] Guid id, [FromBody] UpdateTeacherRequestDto updateTeacherRequestDto)
        {
            logger.LogInformation($"TeachersController >> UpdateV2 >> UpdateTeacherRequestDto:  {JsonSerializer.Serialize(updateTeacherRequestDto)}; ID: {id}");
            if (!ValidateUpdateAsync(updateTeacherRequestDto))
            {
                return BadRequest();
            }
            var record = await teacherRepository.UpdateAsync(id, updateTeacherRequestDto);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // Delete data
        [MapToApiVersion("1.0")]
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteV1([FromRoute] Guid id)
        {
            logger.LogInformation($"TeachersController >> DeleteV1 >> ID: {id}");
            var record = await teacherRepository.DeleteAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // Delete data
        [MapToApiVersion("2.0")]
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteV2([FromRoute] Guid id)
        {
            logger.LogInformation($"TeachersController >> DeleteV1 >> ID: {id}");
            var record = await teacherRepository.DeleteAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        #region Private methods

        private bool ValidateCreateAsync(AddTeacherRequestDto request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(AddTeacherRequestDto),
                   $"{nameof(AddTeacherRequestDto)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(request.TeacherName))
            {
                ModelState.AddModelError(nameof(request.TeacherName),
                    $"{nameof(request.TeacherName)} can not be empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(request.Phone))
            {
                ModelState.AddModelError(nameof(request.Phone),
                    $"{nameof(request.Phone)} can not be empty or white space.");
            }

            int ageOfTeacher = 0;
            var isAgeValid = int.TryParse(request.Age, out ageOfTeacher);

            if (!isAgeValid || ageOfTeacher <= 0)
            {
                ModelState.AddModelError(nameof(request.Age),
                    $"{nameof(request.Age)} can not less than or equals zero.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateAsync(UpdateTeacherRequestDto request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(UpdateTeacherRequestDto),
                   $"{nameof(UpdateTeacherRequestDto)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(request.TeacherName))
            {
                ModelState.AddModelError(nameof(request.TeacherName),
                    $"{nameof(request.TeacherName)} can not be empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(request.Phone))
            {
                ModelState.AddModelError(nameof(request.Phone),
                    $"{nameof(request.Phone)} can not be empty or white space.");
            }

            int ageOfTeacher = 0;
            var isAgeValid = int.TryParse(request.Age, out ageOfTeacher);

            if (!isAgeValid || ageOfTeacher <= 0)
            {
                ModelState.AddModelError(nameof(request.Age),
                    $"{nameof(request.Age)} can not less than or equals zero.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion Private methods
    }
}