using AutoMapper;
using BERoutes.API.Models.Domain;
using BERoutes.API.Models.DTO;
using BERoutes.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BERoutes.API.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository,
            IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();

            var result = mapper.Map<List<RegionDto>>(regions);

            return Ok(result); // Return the list of DTOs
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegion")]
        public async Task<IActionResult> GetRegion(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if (region == null) 
            {
                return NotFound();
            }

            var result = mapper.Map<RegionDto>(region);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion([FromBody] AddRegionRequest request)
        {
            // Validate Request
            //if (!ValidateAddRegion(request))
            //{
            //    return BadRequest(ModelState);
            //}

            // Request(DTO) to Domain model
            var region = new Region()
            { 
                Code = request.Code,
                Area = request.Area,
                Lat = request.Lat,
                Long = request.Long,
                Name = request.Name,
                Population = request.Population
            };

            // Pass details to Repository
            region = await regionRepository.AddAsync(region);

            // Convert back to DTO
            var result = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegion), new { id = result.Id}, result);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion(Guid id, 
            [FromBody] UpdateRegionRequest request)
        {
            // Validate Request
            //if (!ValidateUpdateRegion(request))
            //{
            //    return BadRequest(ModelState);
            //}

            // Convert DTO to Domain model
            var region = new Region()
            {
                Code = request.Code,
                Area = request.Area,
                Lat = request.Lat,
                Long = request.Long,
                Name = request.Name,
                Population = request.Population
            };

            // Update Region using repository
            region = await regionRepository.UpdateAsync(id, region);
            
            // If null return not found
            if (region == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var result = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            // Return Ok response
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            // Get region from Database
            var region = await regionRepository.DeleteAsync(id);

            // If no region return not found
            if (region == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var result = new RegionDto
            {
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            // Return Ok response
            return Ok(result);
        }

        #region Private methods

        private bool ValidateAddRegion(AddRegionRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request),
                    $"Add Region Data is required.");

                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code), 
                    $"{nameof(request.Code)} must cannot be null, empty or white space.");
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), 
                    $"{nameof(request.Name)} must cannot be null, empty or white space.");
            }
            if (request.Area <= 0)
            {
                ModelState.AddModelError(nameof(request.Area), 
                    $"{nameof(request.Area)} must cannot be less than or equal to 0.");
            }
            if (request.Population < 0)
            {
                ModelState.AddModelError(nameof(request.Long), 
                    $"{nameof(request.Population)} must cannot be less than 0.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        private bool ValidateUpdateRegion(UpdateRegionRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request),
                    $"Update Region Data is required.");

                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code), 
                    $"{nameof(request.Code)} must cannot be null, empty or white space.");
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), 
                    $"{nameof(request.Name)} must cannot be null, empty or white space.");
            }
            if (request.Area <= 0)
            {
                ModelState.AddModelError(nameof(request.Area), 
                    $"{nameof(request.Area)} must cannot be less than or equal to 0.");
            }
            if (request.Population < 0)
            {
                ModelState.AddModelError(nameof(request.Long), 
                    $"{nameof(request.Population)} must cannot be less than 0.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion

    }
}
