using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapping;
        private readonly IWalkRepository repository;

        public WalksController(IMapper mapping, IWalkRepository repository)
        {
            this.mapping = mapping;
            this.repository = repository;
        }

        //Create walk POST: /api/Walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {        
                //  Map Dto to Domain
                var walkDomainModel = mapping.Map<Walk>(addWalkRequestDto);

                await repository.CreateAsync(walkDomainModel);

                // Map Domain to Dto
                return Ok(mapping.Map<WalkDto>(walkDomainModel));
        }

        //Get all walks GET: /api/Walks ?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10 ****FILTERING****  ****SORTING**** ****PAGINATION****
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            var walkDomain = await repository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            //  Map Domain to Dto
            return Ok(mapping.Map<List<WalkDto>>(walkDomain));

        }

        //Get walk by Id GET: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getById([FromRoute] Guid id)
        {
            var walkDomainById = await repository.GetByIdAsync(id);

            if(walkDomainById == null)
            {
                return NotFound();
            }

            //  Map Domain to Dto
            return Ok(mapping.Map<WalkDto>(walkDomainById));
        }

        //Update walk by Id PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {         
                //  Map Dto to domain
                Walk walkDomain = mapping.Map<Walk>(updateWalkRequestDto);

                walkDomain = await repository.UpdateAsync(id, walkDomain);

                // Check if it exist
                if (walkDomain == null)
                {
                    return NotFound();
                }

                // Map domain to Dto and return 
                return Ok(mapping.Map<WalkDto>(walkDomain));
        }

        //Delete walk by Id DELETE: /api/Walk/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteWalk = await repository.DeleteAsync(id);

            if(deleteWalk == null)
            {
                return NotFound();
            }

            //  Map Domain to Dto
            return Ok(mapping.Map<WalkDto>(deleteWalk));
        } 
    }
}
