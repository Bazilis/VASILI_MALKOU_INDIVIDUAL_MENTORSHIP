using BLL.Dto;
using FluentValidation;

namespace BLL.Validators
{
    public class WeatherHistoryReaderInputDataValidator : AbstractValidator<WeatherHistoryReaderInputDataDto>
    {
        public WeatherHistoryReaderInputDataValidator()
        {
            RuleFor(x => x.CityName)
                .NotEmpty();

            RuleFor(x => x.StartDate)
                .LessThan(y => y.EndDate)
                .NotEmpty();

            RuleFor(x => x.EndDate)
                .NotEmpty();
        }
    }
}
