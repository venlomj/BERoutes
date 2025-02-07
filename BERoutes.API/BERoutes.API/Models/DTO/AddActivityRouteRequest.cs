namespace BERoutes.API.Models.DTO
{
    public class AddActivityRouteRequest
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid RouteDifficultyId { get; set; }
    }
}
