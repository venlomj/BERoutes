using BERoutes.API.Models.DTO;
using FluentValidation;

namespace BERoutes.API.Validators
{
    public class AddActivityRouteRequestValidator: AbstractValidator<AddActivityRouteRequest>
    {
        public AddActivityRouteRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
