using AutoMapper;
using Azure.Core;
using DemoApp.API.Constants;
using DemoApp.API.Data;
using DemoApp.API.Interfaces;
using DemoApp.API.Models;
using DemoApp.API.Models.DTO.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Text.Json;

namespace DemoApp.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class StudentController : ODataController
    {
        private readonly IStudentRepository studentRepository;

        public ILogger<StudentController> logger { get; }

        public StudentController(IStudentRepository studentRepository, ILogger<StudentController> _logger
            )
        {
            this.studentRepository = studentRepository;
            logger = _logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = $"({Roles.Reader}, {Roles.Writer})")]
        public async Task<ApiResponse> GetAllV1(int pageIndex = 1, int pageSize = 10)
        {
            var studentsDto = await studentRepository.GetAllAsync(pageIndex, pageSize);
            logger.LogInformation($"Finnished get all of students: {JsonSerializer.Serialize(studentsDto)}");
            return new ApiResponse(true, string.Empty, studentsDto);
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = $"({Roles.Reader}, {Roles.Writer})")]
        public async Task<ApiResponse> GetAllV2(int pageIndex = 1, int pageSize = 10)
        {
            var studentsDto = await studentRepository.GetAllAsync(pageIndex, pageSize);
            logger.LogInformation($"Finnshed get all of students: {JsonSerializer.Serialize(studentsDto)}");
            return new ApiResponse(true, string.Empty, studentsDto);
        }

        // Get by ID
        [MapToApiVersion("1.0")]
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = Roles.Writer)]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> GetByID([FromRoute] Guid id)
        {
            var record = await studentRepository.GetAsync(id);
            if (record == null) return new ApiResponse(false, "Not found", new Object());
            return new ApiResponse(true, string.Empty, record);
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = Roles.Writer)]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> GetByIDV2([FromRoute] Guid id)
        {
            var record = await studentRepository.GetAsync(id);
            if (record == null) return new ApiResponse(false, "Not found", new Object());
            return new ApiResponse(true, string.Empty, record);
        }

        // Create new record
        [MapToApiVersion("1.0")]
        [HttpPost]
        [Authorize(Roles = Roles.Writer)]
        public async Task<ApiResponse> Create([FromBody] AddStudentRequestDto request)
        {
            if (!ValidateCreateAsync(request))
            {
                return new ApiResponse(false, "Bad request", new Object());
            }

            var record = await studentRepository.CreateAsync(request);
            if (record == null) return new ApiResponse(false, "Not found", new Object());
            return new ApiResponse(true, string.Empty, record);
        }

        [MapToApiVersion("2.0")]
        [HttpPost]
        [Authorize(Roles = Roles.Writer)]
        public async Task<ApiResponse> CreateV2([FromBody] AddStudentRequestDto request)
        {
            if (!ValidateCreateAsync(request))
            {
                return new ApiResponse(false, "Bad request", new Object());
            }
            var record = await studentRepository.CreateAsync(request);
            if (record == null) return new ApiResponse(false, "Not found", new Object());
            return new ApiResponse(true, string.Empty, record);
        }

        // Update data
        [MapToApiVersion("1.0")]
        [HttpPut]
        [Authorize(Roles = Roles.Reader)]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> Update([FromRoute] Guid id, [FromBody] UpdateStudentRequestDto updateStudentRequestDto)
        {
            if (!ValidateUpdateAsync(updateStudentRequestDto))
            {
                return new ApiResponse(false, "Bad request", new Object());
            }
            var record = await studentRepository.UpdateAsync(id, updateStudentRequestDto);
            if (record == null) return new ApiResponse(false, "Not found", new Object());
            return new ApiResponse(true, string.Empty, record);
        }

        [MapToApiVersion("2.0")]
        [HttpPut]
        [Authorize(Roles = Roles.Reader)]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> UpdateV2([FromRoute] Guid id, [FromBody] UpdateStudentRequestDto updateStudentRequestDto)
        {
            if (!ValidateUpdateAsync(updateStudentRequestDto))
            {
                return new ApiResponse(false, "Bad request", new Object());
            }
            var record = await studentRepository.UpdateAsync(id, updateStudentRequestDto);
            if (record == null) return new ApiResponse(false, "Not found", new Object());
            return new ApiResponse(true, string.Empty, record);
        }

        // Delete data
        [MapToApiVersion("1.0")]
        [HttpDelete]
        [Authorize(Roles = Roles.Writer)]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> Delete([FromRoute] Guid id)
        {
            var record = await studentRepository.DeleteAsync(id);

            if (record == null) return new ApiResponse(false, "Not found", new Object());

            return new ApiResponse(true, string.Empty, record);
        }

        [MapToApiVersion("2.0")]
        [HttpDelete]
        [Authorize(Roles = Roles.Writer)]
        [Route("{id:Guid}")]
        public async Task<ApiResponse> DeleteV2([FromRoute] Guid id)
        {
            var record = await studentRepository.DeleteAsync(id);
            if (record == null) return new ApiResponse(false, "Not found", new Object());
            return new ApiResponse(true, string.Empty, record);
        }

        #region Private methods

        private bool ValidateCreateAsync(AddStudentRequestDto request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(AddStudentRequestDto),
                   $"{nameof(AddStudentRequestDto)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                ModelState.AddModelError(nameof(request.FirstName),
                    $"{nameof(request.FirstName)} can not be empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                ModelState.AddModelError(nameof(request.LastName),
                    $"{nameof(request.LastName)} can not be empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                ModelState.AddModelError(nameof(request.PhoneNumber),
                    $"{nameof(request.PhoneNumber)} can not be empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                ModelState.AddModelError(nameof(request.Email),
                    $"{nameof(request.Email)} can not be empty or white space.");
            }

            if (request.Old <= 0)
            {
                ModelState.AddModelError(nameof(request.Old),
                    $"{nameof(request.Old)} can not less than or equals zero.");
            }

            if (string.IsNullOrWhiteSpace(request.AvataUrl))
            {
                ModelState.AddModelError(nameof(request.AvataUrl),
                    $"{nameof(request.AvataUrl)} can not be empty or white space.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateAsync(UpdateStudentRequestDto request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(UpdateStudentRequestDto),
                   $"{nameof(UpdateStudentRequestDto)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                ModelState.AddModelError(nameof(request.FirstName),
                    $"{nameof(request.FirstName)} can not be empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                ModelState.AddModelError(nameof(request.LastName),
                    $"{nameof(request.LastName)} can not be empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                ModelState.AddModelError(nameof(request.PhoneNumber),
                    $"{nameof(request.PhoneNumber)} can not be empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                ModelState.AddModelError(nameof(request.Email),
                    $"{nameof(request.Email)} can not be empty or white space.");
            }

            if (request.Old <= 0)
            {
                ModelState.AddModelError(nameof(request.Old),
                    $"{nameof(request.Old)} can not less than or equals zero.");
            }

            if (string.IsNullOrWhiteSpace(request.AvataUrl))
            {
                ModelState.AddModelError(nameof(request.AvataUrl),
                    $"{nameof(request.AvataUrl)} can not be empty or white space.");
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