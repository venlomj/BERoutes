using BERoutes.API.Models.DTO;
using FluentValidation;

namespace BERoutes.API.Validators
{
    public class UpdateRouteDifficultyRequestValidator : AbstractValidator<UpdateRouteDifficultyRequest>
    {
        public UpdateRouteDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
