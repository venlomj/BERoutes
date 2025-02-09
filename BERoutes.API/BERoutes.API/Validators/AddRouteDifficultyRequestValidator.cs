using BERoutes.API.Models.DTO;
using FluentValidation;

namespace BERoutes.API.Validators
{
    public class AddRouteDifficultyRequestValidator : AbstractValidator<AddRouteDifficultyRequest>
    {
        public AddRouteDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
