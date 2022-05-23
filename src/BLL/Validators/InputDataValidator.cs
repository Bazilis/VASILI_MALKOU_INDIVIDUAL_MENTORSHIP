using BLL.Dto;
using FluentValidation;

namespace BLL.Validators
{
    public class InputDataValidator : AbstractValidator<InputDataDto>
    {
        public InputDataValidator()
        {
            RuleFor(x => x.CityName)
                .NotEmpty();

            RuleFor(x => x.NumberOfDays)
                .InclusiveBetween(1, 16)
                .NotEmpty();
        }
    }
}
