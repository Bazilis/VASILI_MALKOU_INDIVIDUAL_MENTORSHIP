using BLL.Dto;
using FluentValidation;

namespace BLL.Validators
{
    public class WeatherForecastInputDataValidator : AbstractValidator<WeatherForecastInputDataDto>
    {
        public WeatherForecastInputDataValidator(int minDays, int maxDays)
        {
            RuleFor(x => x.CityName)
                .NotEmpty();

            RuleFor(x => x.NumberOfDays)
                .InclusiveBetween(minDays, maxDays);
        }
    }
}
