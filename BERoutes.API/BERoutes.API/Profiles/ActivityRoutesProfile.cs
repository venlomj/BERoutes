using AutoMapper;
using BERoutes.API.Models.Domain;
using BERoutes.API.Models.DTO;

namespace BERoutes.API.Profiles
{
    public class ActivityRoutesProfile: Profile
    {
        public ActivityRoutesProfile() 
        {
            CreateMap<ActivityRoute, ActivityRouteDto>()
                .ReverseMap();

            CreateMap<RouteDifficulty, RouteDifficultyDto>()
                .ReverseMap();
        }
    }
}
