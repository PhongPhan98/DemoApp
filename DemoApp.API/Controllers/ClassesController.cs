using AutoMapper;
using Azure.Core;
using DemoApp.API.Data;
using DemoApp.API.Interfaces;
using DemoApp.API.Models;
using DemoApp.API.Models.DTO.Classes;
using DemoApp.API.Models.DTO.Students;
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
    public class ClassesController : ODataController
    {
        private readonly DemoAppDbContext dbContext;
        private readonly IClassRepository classRepository;
        private readonly IMapper mapper;

        public ILogger<StudentController> logger { get; }

        public ClassesController(DemoAppDbContext dbContext, IClassRepository classRepository, IMapper mapper, ILogger<StudentController> _logger)
        {
            this.dbContext = dbContext;
            this.classRepository = classRepository;
            this.mapper = mapper;
            logger = _logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [EnableQuery]
        public async Task<ApiResponse> GetAllV1(int pageIndex = 1, int pageSize = 10)
        {
            logger.LogInformation($"ClassesController >> GetAllV1 >>  pageIndex :{pageIndex},  pageSize: {pageSize}");
            var classes = await classRepository.GetAllAsync(pageIndex, pageSize);
            logger.LogInformation($"ClassesController >> GetAllV1 >> Finnished get all of classes: {JsonSerializer.Serialize(classes)}");
            return new ApiResponse(true, string.Empty, classes);
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        [EnableQuery]
        public async Task<ApiResponse> GetAllV2(int pageIndex = 1, int pageSize = 10)
        {
            logger.LogInformation($"ClassesController >> GetAllV2 >>  pageIndex :{pageIndex},  pageSize: {pageSize}");
            var classes = await classRepository.GetAllAsync(pageIndex, pageSize);
            logger.LogInformation($"ClassesController >> GetAllV2 >> Finnished get all of classes: {JsonSerializer.Serialize(classes)}");
            return new ApiResponse(true, string.Empty, classes);
        }

        // Get by ID
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIDV1([FromRoute] Guid id)
        {
            logger.LogInformation($"ClassesController >> GetByIDV1 >> ID: {id}");
            var record = await classRepository.GetAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // Get by ID
        [MapToApiVersion("2.0")]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIDV2([FromRoute] Guid id)
        {
            logger.LogInformation($"ClassesController >> GetByIDV2 >> ID: {id}");
            var record = await classRepository.GetAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // Create new record
        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> CreateV1([FromBody] AddClassRequestDto request)
        {
            logger.LogInformation($"ClassesController >> CreateV1 >> AddClassRequestDto:  {JsonSerializer.Serialize(request)}");
            if (!ValidateCreateAsync(request))
            {
                return BadRequest();
            }
            var record = await classRepository.CreateAsync(request);
            if (record == null) return NotFound();
            return CreatedAtAction(nameof(CreateV1), new { id = record.ClassId }, record);
        }

        // Create new record
        [MapToApiVersion("2.0")]
        [HttpPost]
        public async Task<IActionResult> CreateV2([FromBody] AddClassRequestDto request)
        {
            logger.LogInformation($"ClassesController >> CreateV2 >> AddClassRequestDto:  {JsonSerializer.Serialize(request)}");
            if (!ValidateCreateAsync(request))
            {
                return BadRequest();
            }
            var record = await classRepository.CreateAsync(request);
            if (record == null) return NotFound();
            return CreatedAtAction(nameof(CreateV2), new { id = record.ClassId }, record);
        }

        // Update data
        [MapToApiVersion("1.0")]
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateV1([FromRoute] Guid id, [FromBody] UpdateClassRequestDto updateClassRequestDto)
        {
            logger.LogInformation($"ClassesController >> UpdateV1 >> UpdateStudentRequestDto:  {JsonSerializer.Serialize(updateClassRequestDto)}; ID: {id}");
            if (!ValidateUpdateAsync(updateClassRequestDto))
            {
                return BadRequest();
            }
            var record = await classRepository.UpdateAsync(id, updateClassRequestDto);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // Update data
        [MapToApiVersion("2.0")]
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateV2([FromRoute] Guid id, [FromBody] UpdateClassRequestDto updateClassRequestDto)
        {
            logger.LogInformation($"ClassesController >> UpdateV2 >> UpdateStudentRequestDto:  {JsonSerializer.Serialize(updateClassRequestDto)}; ID: {id}");
            if (!ValidateUpdateAsync(updateClassRequestDto))
            {
                return BadRequest();
            }
            var record = await classRepository.UpdateAsync(id, updateClassRequestDto);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // Delete data
        [MapToApiVersion("1.0")]
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteV1([FromRoute] Guid id)
        {
            logger.LogInformation($"ClassesController >> DeleteV1 >> ID: {id}");
            var record = await classRepository.DeleteAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // Delete data
        [MapToApiVersion("2.0")]
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteV2([FromRoute] Guid id)
        {
            logger.LogInformation($"ClassesController >> DeleteV2 >> ID: {id}");
            var record = await classRepository.DeleteAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        #region Private methods

        private bool ValidateCreateAsync(AddClassRequestDto request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(AddClassRequestDto),
                   $"{nameof(AddClassRequestDto)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(request.ClassName))
            {
                ModelState.AddModelError(nameof(request.ClassName),
                    $"{nameof(request.ClassName)} can not be empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(request.Description))
            {
                ModelState.AddModelError(nameof(request.Description),
                    $"{nameof(request.Description)} can not be empty or white space.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateAsync(UpdateClassRequestDto request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(UpdateClassRequestDto),
                   $"{nameof(UpdateClassRequestDto)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(request.ClassName))
            {
                ModelState.AddModelError(nameof(request.ClassName),
                    $"{nameof(request.ClassName)} can not be empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(request.Description))
            {
                ModelState.AddModelError(nameof(request.Description),
                    $"{nameof(request.Description)} can not be empty or white space.");
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