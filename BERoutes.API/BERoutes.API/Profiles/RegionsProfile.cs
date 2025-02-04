using AutoMapper;
using BERoutes.API.Models.Domain;
using BERoutes.API.Models.DTO;

namespace BERoutes.API.Profiles
{
    public class RegionsProfile: Profile
    {
        public RegionsProfile()
        {
            CreateMap<Region, RegionDto>()
                .ReverseMap(); 
        }
    }
}
