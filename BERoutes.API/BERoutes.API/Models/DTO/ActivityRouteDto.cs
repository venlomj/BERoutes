using BERoutes.API.Models.Domain;

namespace BERoutes.API.Models.DTO
{
    public class ActivityRouteDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }


        public Guid RegionId { get; set; }
        public Guid RouteDifficultyId { get; set; }

        // Navigation properties
        public RouteDifficultyDto RouteDifficulty { get; set; }
        public RegionDto Region { get; set; }
    }
}
