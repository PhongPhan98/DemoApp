using AutoMapper;
using DemoApp.API.Data;
using DemoApp.API.Interfaces;
using DemoApp.API.Models;
using DemoApp.API.Models.DTO.Students;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DemoAppDbContext dbContext;
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentController(DemoAppDbContext dbContext, IStudentRepository studentRepository, IMapper mapper
            )
        {
            this.dbContext = dbContext;
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ApiResponse> GetAll(int pageIndex = 1, int pageSize = 10)
        {
            var studentsDto = await studentRepository.GetAllAsync(pageIndex, pageSize);
            return new ApiResponse(true, null, studentsDto);
        }

        // Get by ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> GetByID([FromRoute] Guid id)
        {
            var record = await studentRepository.GetAsync(id);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return new ApiResponse(true, null, record);

        }

        // Create new record
        [HttpPost]
        public async Task<ApiResponse> Create([FromBody] AddStudentRequestDto request)
        {

            var record = await studentRepository.CreateAsync(request);
            if (record == null)  return new ApiResponse(false, "Not found", null);
            return new ApiResponse(true, null, record);
        }

        // Update data
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> Update([FromRoute] Guid id, [FromBody] UpdateStudentRequestDto updateStudentRequestDto)
        {
            var record = await studentRepository.UpdateAsync(id, updateStudentRequestDto);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return  new ApiResponse(true, null, record);
        }


        // Delete data
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> Delete([FromRoute] Guid id)
        {
            var record = await studentRepository.DeleteAsync(id);
            if (record == null) return new ApiResponse(false, "Not found", null);
            return  new ApiResponse(true, null, record);
        }
    }
}
