using AutoMapper;
using BERoutes.API.Models.Domain;
using BERoutes.API.Models.DTO;
using BERoutes.API.Repositories;
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

        public ActivityRoutesController(IActivityRouteRepository activityRouteRepository,
            IMapper mapper)
        {
            this.activityRouteRepository = activityRouteRepository;
            this.mapper = mapper;
        }

        [HttpGet]
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
        public async Task<IActionResult> GetActivityRoute(Guid id)
        {
            var activityRoute = await activityRouteRepository.GetAsync(id);

            if (activityRoute == null)
            {
                return NotFound();
            }

            var result = mapper.Map<ActivityRouteDto>(activityRoute);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddActivityRoute([FromBody] AddActivityRouteRequest request)
        {
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
        public async Task<IActionResult> UpdateActivityRoute([FromRoute] Guid id, [FromBody] UpdateActivityRouteRequest request)
        {
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
    }
}
