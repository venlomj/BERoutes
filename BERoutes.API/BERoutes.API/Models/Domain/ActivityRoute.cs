namespace BERoutes.API.Models.Domain
{
    public class ActivityRoute
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }


        public Guid RegionId { get; set; }
        public Guid RouteDifficultyId { get; set; }

        // Navigation properties
        public RouteDifficulty RouteDifficulty { get; set; }
        public Region Region { get; set; }
    }
}
