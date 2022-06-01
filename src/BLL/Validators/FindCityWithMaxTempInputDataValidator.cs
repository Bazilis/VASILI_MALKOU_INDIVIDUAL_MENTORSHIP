using BLL.Dto;
using FluentValidation;

namespace BLL.Validators
{
    public class FindCityWithMaxTempInputDataValidator : AbstractValidator<FindCityWithMaxTempInputDataDto>
    {
        public FindCityWithMaxTempInputDataValidator()
        {
            RuleFor(x => x.Cities)
                .NotEmpty();
        }
    }
}
