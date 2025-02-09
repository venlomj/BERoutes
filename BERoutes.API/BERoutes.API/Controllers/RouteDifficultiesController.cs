using AutoMapper;
using BERoutes.API.Models.Domain;
using BERoutes.API.Models.DTO;
using BERoutes.API.Repositories.Implementations;
using BERoutes.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BERoutes.API.Controllers
{
    [Route("api/route-difficulties")]
    [ApiController]
    public class RouteDifficultiesController : ControllerBase
    {
        private readonly IRouteDifficultyRepository routeDifficultyRepository;
        private readonly IMapper mapper;

        public RouteDifficultiesController(IRouteDifficultyRepository routeDifficultyRepository,
            IMapper mapper)
        {
            this.routeDifficultyRepository = routeDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRouteDifficulties()
        {
            // Fatch data from the database
            var routeDifficulties = await routeDifficultyRepository.GetAllAsync();

            // Convert domain routeDifficulties to DTO
            var result = mapper.Map<List<RouteDifficultyDto>>(routeDifficulties);

            // Return response
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRouteDifficulty")]
        public async Task<IActionResult> GetRouteDifficulty(Guid id)
        {
            var routeDifficulty = await routeDifficultyRepository.GetAsync(id);

            if (routeDifficulty == null)
            {
                return NotFound($"Route Difficulty with the {id}, not found");
            }

            // Convert Domain to DTO
            var result = mapper.Map<RouteDifficultyDto>(routeDifficulty);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion([FromBody] AddRouteDifficultyRequest request)
        {
            // Validate Request
            if (!ValidateAddRouteDifficulty(request))
            {
                return BadRequest(ModelState);
            }

            // Request(DTO) to Domain model
            var routeDifficulty = new RouteDifficulty
            {
                Code = request.Code
            };

            // Pass details to Repository
            routeDifficulty = await routeDifficultyRepository.AddAsync(routeDifficulty);

            // Convert back to DTO
            var result = mapper.Map<RouteDifficultyDto>(routeDifficulty);

            return CreatedAtAction(nameof(GetRouteDifficulty), new { id = result.Id }, result);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRouteDifficulty(Guid id, UpdateRouteDifficultyRequest request)
        {
            // Validate Request
            if (!ValidateUpdateRouteDifficulty(request))
            {
                return BadRequest(ModelState);
            }

            var existingRouteDifficulty = new RouteDifficulty
            {
                Code = request.Code,
            };

            // Call repository to update
            existingRouteDifficulty = await routeDifficultyRepository.UpdateAsync(id, existingRouteDifficulty);



            if (existingRouteDifficulty == null)
            {
                return NotFound($"Route Difficulty with the {id}, not found");
            }

            // Convert Domain to DTO
            var result = mapper.Map<RouteDifficultyDto>(existingRouteDifficulty);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRouteDifficulty([FromRoute] Guid id)
        {
            // Call Repository to delete RouteDifficulty
            var routeDifficulty = await routeDifficultyRepository.DeleteAsync(id);

            if (routeDifficulty == null)
            {
                return NotFound($"Route Difficulty with the {id}, not found");
            }

            var result = mapper.Map<RouteDifficultyDto>(routeDifficulty);

            return Ok(result);
        }

        #region Private methods

        private bool ValidateAddRouteDifficulty(AddRouteDifficultyRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request),
                    $"{nameof(request)} cannot be empty.");

                return false;
            }
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code),
                    $"{nameof(request.Code)} is required.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        private bool ValidateUpdateRouteDifficulty(UpdateRouteDifficultyRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request),
                    $"{nameof(request)} cannot be empty.");

                return false;
            }
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code),
                    $"{nameof(request.Code)} is required.");
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
