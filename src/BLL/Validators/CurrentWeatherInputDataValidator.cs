using BLL.Dto;
using FluentValidation;

namespace BLL.Validators
{
    public class CurrentWeatherInputDataValidator : AbstractValidator<CurrentWeatherInputDataDto>
    {
        public CurrentWeatherInputDataValidator()
        {
            RuleFor(x => x.CityName)
                .NotEmpty();
        }
    }
}
