using AutoMapper;
using DemoApp.API.Data;
using DemoApp.API.Interfaces;
using DemoApp.API.Models;
using DemoApp.API.Models.DTO.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DemoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<ApiResponse> GetAll(int pageIndex = 1, int pageSize = 10)
        {
            var studentsDto = await studentRepository.GetAllAsync(pageIndex, pageSize);
            logger.LogInformation($"Finnshed get all of students: {JsonSerializer.Serialize(studentsDto)}");
            return new ApiResponse(true, null, studentsDto);
        }

        // Get by ID
        [HttpGet]
        [Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> GetByID([FromRoute] Guid id)
        {
            var record = await studentRepository.GetAsync(id);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return new ApiResponse(true, null, record);
        }

        // Create new record
        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<ApiResponse> Create([FromBody] AddStudentRequestDto request)
        {
            var record = await studentRepository.CreateAsync(request);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return new ApiResponse(true, null, record);
        }

        // Update data
        [HttpPut]
        [Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> Update([FromRoute] Guid id, [FromBody] UpdateStudentRequestDto updateStudentRequestDto)
        {
            var record = await studentRepository.UpdateAsync(id, updateStudentRequestDto);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return new ApiResponse(true, null, record);
        }

        // Delete data
        [HttpDelete]
        [Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> Delete([FromRoute] Guid id)
        {
            var record = await studentRepository.DeleteAsync(id);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return new ApiResponse(true, null, record);
        }
    }
}