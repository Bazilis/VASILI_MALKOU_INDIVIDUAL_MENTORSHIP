using BLL.Dto;
using FluentValidation;

namespace BLL.Validators
{
    public class WeatherForecastInputDataValidator : AbstractValidator<WeatherForecastInputDataDto>
    {
        public WeatherForecastInputDataValidator()
        {
            RuleFor(x => x.CityName)
                .NotEmpty();

            RuleFor(x => x.NumberOfDays)
                .InclusiveBetween(1, 16)
                .NotEmpty();
        }
    }
}
