using AutoMapper;
using BERoutes.API.Models.Domain;
using BERoutes.API.Models.DTO;
using BERoutes.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BERoutes.API.Controllers
{
    [Route("api/activity-routes")]
    [ApiController]
    public class ActivityRoutesController : ControllerBase
    {
        private readonly IActivityRouteRepository activityRouteRepository;
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IRouteDifficultyRepository routeDifficultyRepository;

        public ActivityRoutesController(IActivityRouteRepository activityRouteRepository,
            IMapper mapper,
            IRegionRepository regionRepository,
            IRouteDifficultyRepository routeDifficultyRepository)
        {
            this.activityRouteRepository = activityRouteRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.routeDifficultyRepository = routeDifficultyRepository;
        }

        [HttpGet]
        [Authorize(Roles = "reader, admin")]
        public async Task<IActionResult> GetAllActivityRoutes()
        {
            // Fatch data from the database
            var activityRoutes = await activityRouteRepository.GetAllAsync();

            // Convert domain activityRoutes to DTO
            var result = mapper.Map<List<ActivityRouteDto>>(activityRoutes);

            // Return response
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetActivityRoute")]
        [Authorize(Roles = "reader, admin")]
        public async Task<IActionResult> GetActivityRoute(Guid id)
        {
            var activityRoute = await activityRouteRepository.GetAsync(id);

            if (activityRoute == null)
            {
                return NotFound($"Activity Route with the {id}, not found");
            }

            var result = mapper.Map<ActivityRouteDto>(activityRoute);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddActivityRoute([FromBody] AddActivityRouteRequest request)
        {
            // Validate Request
            if (!await ValidateAddActivityRoute(request))
            {
                return BadRequest(ModelState);
            }

            // Convert DTO to Domain Object
            var activityRoute = new ActivityRoute
            {
                Length = request.Length,
                Name = request.Name,
                RegionId = request.RegionId,
                RouteDifficultyId = request.RouteDifficultyId,
            };

            // Pass domain object to Repository to persist this
            activityRoute = await activityRouteRepository.AddAsync(activityRoute);

            // Convert domain object back to DTO
            var result = new ActivityRoute
            {
                Id = activityRoute.Id,
                Name = activityRoute.Name,
                Length = activityRoute.Length,
                RegionId = activityRoute.RegionId,
                RouteDifficultyId = activityRoute.RouteDifficultyId,
            };

            // Send DTO response back to Client
            return CreatedAtAction(nameof(GetActivityRoute), new { id = result.Id}, result);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateActivityRoute([FromRoute] Guid id, [FromBody] UpdateActivityRouteRequest request)
        {
            // Validate Request
            if (!await ValidateUpdateActivityRoute(request))
            {
                return BadRequest(ModelState);
            }

            // Convert DTO to Domain object
            var activityRoute = new ActivityRoute
            {
                Length = request.Length,
                Name = request.Name,
                RegionId = request.RegionId,
                RouteDifficultyId = request.RouteDifficultyId,
            };

            // Pass details to Repository - Get Domain object in response (or null)
            activityRoute = await activityRouteRepository.UpdateAsync(id, activityRoute);

            // Handle null (Not Found)
            if (activityRoute == null)
            {
                return NotFound($"Activity Route with the {id}, not found");
            }

            // Convert back Domain to DTO
            var result = new ActivityRouteDto
            {
                Id = activityRoute.Id,
                Name = activityRoute.Name,
                Length = activityRoute.Length,
                RegionId = activityRoute.RegionId,
                RouteDifficultyId = activityRoute.RouteDifficultyId,
            };

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteActivityRoute([FromRoute] Guid id)
        {
            // Call Repository to delete ActivityRoute
            var activityRoute = await activityRouteRepository.DeleteAsync(id);

            if (activityRoute == null)
            {
                return NotFound($"Activity Route with the {id}, not found");
            }

            var result = mapper.Map<ActivityRouteDto>(activityRoute);

            return Ok(result);
        }

        #region Private methods

        private async Task<bool> ValidateAddActivityRoute(AddActivityRouteRequest request)
        {
            //if (request == null)
            //{
            //    ModelState.AddModelError(nameof(request),
            //        $"{nameof(request)} cannot be empty.");

            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(request.Name))
            //{
            //    ModelState.AddModelError(nameof(request.Name),
            //        $"{nameof(request.Name)} is required.");
            //}
            //if (request.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(request.Length),
            //        $"{nameof(request.Length)} should be greather than zero.");
            //}

            var region = await regionRepository.GetAsync(request.RegionId);
            if (region == null) 
            {
                ModelState.AddModelError(nameof(request.RegionId),
                                    $"{nameof(request.RegionId)} is invalid");
            }

            var routeDifficulty = await routeDifficultyRepository.GetAsync(request.RouteDifficultyId);
            if (routeDifficulty == null) 
            {
                ModelState.AddModelError(nameof(request.RouteDifficultyId),
                                    $"{nameof(request.RouteDifficultyId)} is invalid");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> ValidateUpdateActivityRoute(UpdateActivityRouteRequest request)
        {
            //if (request == null)
            //{
            //    ModelState.AddModelError(nameof(request),
            //        $"{nameof(request)} cannot be empty.");

            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(request.Name))
            //{
            //    ModelState.AddModelError(nameof(request.Name),
            //        $"{nameof(request.Name)} is required.");
            //}
            //if (request.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(request.Length),
            //        $"{nameof(request.Length)} should be greather.");
            //}

            var region = await regionRepository.GetAsync(request.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(request.RegionId),
                                    $"{nameof(request.RegionId)} is invalid");
            }

            var routeDifficulty = await routeDifficultyRepository.GetAsync(request.RouteDifficultyId);
            if (routeDifficulty == null)
            {
                ModelState.AddModelError(nameof(request.RouteDifficultyId),
                                    $"{nameof(request.RouteDifficultyId)} is invalid");
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
