using AutoMapper;
using DemoApp.API.Constants;
using DemoApp.API.Data;
using DemoApp.API.Interfaces;
using DemoApp.API.Models;
using DemoApp.API.Models.DTO.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DemoApp.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public ILogger<StudentController> logger { get; }

        public StudentController(IStudentRepository studentRepository, IMapper mapper, ILogger<StudentController> _logger
            )
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            logger = _logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Authorize(Roles = $"({Roles.Reader}, {Roles.Writer})")]
        public async Task<ApiResponse> GetAllV1(int pageIndex = 1, int pageSize = 10)
        {
            var studentsDto = await studentRepository.GetAllAsync(pageIndex, pageSize);
            logger.LogInformation($"Finnished get all of students: {JsonSerializer.Serialize(studentsDto)}");
            return new ApiResponse(true, null, studentsDto);
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        [Authorize(Roles = $"({Roles.Reader}, {Roles.Writer})")]
        public async Task<ApiResponse> GetAllV2(int pageIndex = 1, int pageSize = 10)
        {
            var studentsDto = await studentRepository.GetAllAsync(pageIndex, pageSize);
            logger.LogInformation($"Finnshed get all of students: {JsonSerializer.Serialize(studentsDto)}");
            return new ApiResponse(true, null, studentsDto);
        }

        // Get by ID
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Authorize(Roles = Roles.Writer)]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> GetByID([FromRoute] Guid id)
        {
            var record = await studentRepository.GetAsync(id);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return new ApiResponse(true, null, record);
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        [Authorize(Roles = Roles.Writer)]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> GetByIDV2([FromRoute] Guid id)
        {
            var record = await studentRepository.GetAsync(id);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return new ApiResponse(true, null, record);
        }

        // Create new record
        [MapToApiVersion("1.0")]
        [HttpPost]
        [Authorize(Roles = Roles.Writer)]
        public async Task<ApiResponse> Create([FromBody] AddStudentRequestDto request)
        {
            var record = await studentRepository.CreateAsync(request);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return new ApiResponse(true, null, record);
        }

        [MapToApiVersion("2.0")]
        [HttpPost]
        [Authorize(Roles = Roles.Writer)]
        public async Task<ApiResponse> CreateV2([FromBody] AddStudentRequestDto request)
        {
            var record = await studentRepository.CreateAsync(request);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return new ApiResponse(true, null, record);
        }

        // Update data
        [MapToApiVersion("1.0")]
        [HttpPut]
        [Authorize(Roles = Roles.Reader)]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> Update([FromRoute] Guid id, [FromBody] UpdateStudentRequestDto updateStudentRequestDto)
        {
            var record = await studentRepository.UpdateAsync(id, updateStudentRequestDto);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return new ApiResponse(true, null, record);
        }

        [MapToApiVersion("2.0")]
        [HttpPut]
        [Authorize(Roles = Roles.Reader)]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> UpdateV2([FromRoute] Guid id, [FromBody] UpdateStudentRequestDto updateStudentRequestDto)
        {
            var record = await studentRepository.UpdateAsync(id, updateStudentRequestDto);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return new ApiResponse(true, null, record);
        }

        // Delete data
        [MapToApiVersion("1.0")]
        [HttpDelete]
        [Authorize(Roles = Roles.Writer)]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> Delete([FromRoute] Guid id)
        {
            var record = await studentRepository.DeleteAsync(id);

            if (record == null) return new ApiResponse(false, "Not found", null);

            return new ApiResponse(true, null, record);
        }

        [MapToApiVersion("2.0")]
        [HttpDelete]
        [Authorize(Roles = Roles.Writer)]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> DeleteV2([FromRoute] Guid id)
        {
            var record = await studentRepository.DeleteAsync(id);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return new ApiResponse(true, null, record);
        }
    }
}