﻿using AutoMapper;
using DemoApp.API.Data;
using DemoApp.API.Interfaces;
using DemoApp.API.Models;
using DemoApp.API.Models.DTO.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

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

        public ClassesController(DemoAppDbContext dbContext, IClassRepository classRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.classRepository = classRepository;
            this.mapper = mapper;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [EnableQuery]
        public async Task<ApiResponse> GetAllV1(int pageIndex = 1, int pageSize = 10)
        {
            var classes = await classRepository.GetAllAsync(pageIndex, pageSize);
            return new ApiResponse(true, string.Empty, classes);
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        [EnableQuery]
        public async Task<ApiResponse> GetAllV2(int pageIndex = 1, int pageSize = 10)
        {
            var classes = await classRepository.GetAllAsync(pageIndex, pageSize);
            return new ApiResponse(true, string.Empty, classes);
        }

        // Get by ID
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIDV1([FromRoute] Guid id)
        {
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
            var record = await classRepository.GetAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // Create new record
        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> CreateV1([FromBody] AddClassRequestDto request)
        {
            var record = await classRepository.CreateAsync(request);
            if (record == null) return NotFound();
            return CreatedAtAction(nameof(CreateV1), new { id = record.ClassId }, record);
        }

        // Create new record
        [MapToApiVersion("2.0")]
        [HttpPost]
        public async Task<IActionResult> CreateV2([FromBody] AddClassRequestDto request)
        {
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
            var record = await classRepository.DeleteAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }
    }
}