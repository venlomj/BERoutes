using BERoutes.API.Models.DTO;
using FluentValidation;

namespace BERoutes.API.Validators
{
    public class UpdateActivityRouteRequestValidator: AbstractValidator<UpdateActivityRouteRequest>
    {
        public UpdateActivityRouteRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
