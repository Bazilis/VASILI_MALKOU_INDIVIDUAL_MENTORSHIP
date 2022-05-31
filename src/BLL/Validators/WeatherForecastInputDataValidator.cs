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

            RuleFor(x => x.MinNumberDays)
                .Cascade(CascadeMode.Stop)
                .LessThan(y => y.MaxNumberDays)
                .InclusiveBetween(1, 16);

            RuleFor(x => x.MaxNumberDays)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(y => y.MinNumberDays)
                .InclusiveBetween(1, 16);

            RuleFor(x => x.NumberOfDays)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(y => y.MinNumberDays)
                .LessThanOrEqualTo(z => z.MaxNumberDays);
        }
    }
}
