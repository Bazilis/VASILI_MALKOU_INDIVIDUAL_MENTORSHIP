using BLL.Dto;
using FluentValidation;

namespace BLL.Validators
{
    public class WeatherForecastInputDataValidator : AbstractValidator<WeatherForecastInputDataDto>
    {
        public WeatherForecastInputDataValidator(int minDays, int maxDays)
        {
            //var config = System.Configuration().

            //Configuration config = ConfigurationManager.;

            //var config = new ConfigurationBuilder()
            //    .SetBasePath(AppContext.BaseDirectory)
            //    .AddJsonFile("configservices.json", false, true)
            //    .Build();

            RuleFor(x => x.CityName)
                .NotEmpty();

            RuleFor(x => x.NumberOfDays)
                .InclusiveBetween(minDays, maxDays)
                .NotEmpty();
        }
    }
}
