using DemoApp.API.Data;
using DemoApp.API.Models.Domain;
using DemoApp.API.Models.DTO;
using DemoApp.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace DemoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly DemoAppDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(DemoAppDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var regionsDomain = await regionRepository.GetAllAsync();

            var regionsDto = new List<RegionDto>();

            foreach(var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(regionsDto);

        }


        // Get by ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var record = await regionRepository.GetAsync(id);
            if(record == null) return NotFound();
            return Ok(record);

        }

        // Create new record
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto request)
        {
        
            var record = await regionRepository.CreateAsync(request);
            if (record == null) return NotFound();
            return CreatedAtAction(nameof(Create), new { id = record.Id }, record);
        }

        // Update data
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var record = await regionRepository.UpdateAsync(id, updateRegionRequestDto);
            if(record == null) return NotFound();
            return Ok(record); 
        }


        // Delete data
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var record = await regionRepository.DeleteAsync(id);
            if(record == null) return NotFound();
            return Ok(record);
        }
    }
}
