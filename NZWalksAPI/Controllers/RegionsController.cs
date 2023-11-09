﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //Injections:
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository repository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository repository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.repository = repository;
            this.mapper = mapper;
        }

        //CRUD:

        //GET:
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from database to Domain model:
            var regions = await repository.GetAllAsync();

            //Map Domain to DTO and return
            return Ok(mapper.Map<List<RegionDTO>>(regions));
        }

        //GET(ID):
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get data from database to domain model:
            var region = await repository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDTO>(region));
        }

        // CREATE:
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Convert DTO to Domain model:
            var region = mapper.Map<Region>(addRegionRequestDto);
            

            //Use Domain model to create a new region:
            region = await repository.CreateAsync(region);

            //Return an answer to the client with Dto:
            var regionDto = mapper.Map<RegionDTO>(region);

            return CreatedAtAction(nameof(GetById), new {id =  region.Id}, regionDto);
        }

        //UPDATE:
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Transform Dto to model:
            Region regionUpdate = mapper.Map<Region>(updateRegionRequestDto);
            
            //Check if region exists:
            regionUpdate = await repository.UpdateAsync(id, regionUpdate);

            if (regionUpdate == null)
            {
                return NotFound();
            }

            //Map Region model to Dto region and return an answer to the client with Dto:
            return Ok(mapper.Map<RegionDTO>(regionUpdate));
        }

        //DELETE:
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await repository.DeleteAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            //Map region to DTO and show it to client:       
            return Ok(mapper.Map<RegionDTO>(region)); ;
        }

    }
}