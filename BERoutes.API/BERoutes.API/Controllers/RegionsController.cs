using AutoMapper;
using BERoutes.API.Models.Domain;
using BERoutes.API.Models.DTO;
using BERoutes.API.Repositories;
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

    }
}
