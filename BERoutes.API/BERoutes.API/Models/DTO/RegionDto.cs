using BERoutes.API.Models.Domain;

namespace BERoutes.API.Models.DTO
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public double Population { get; set; }

        //// Navigation Properties
        //public IEnumerable<ActivityRouteDto> ActivityRoutesDto { get; set; }
    }
}
