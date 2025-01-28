using Dashboard_Management.Constants;
using Dashboard_Management.DTOs;
using FluentValidation;

namespace Dashboard_Management.Validators.Energy
{
    public class MetricRequestValidator : AbstractValidator<MetricRequestDto>
    {
        public MetricRequestValidator()
        {
            RuleFor(x => x.MetricType)
                .NotEmpty().WithMessage(ValidationMessages.MetricTypeRequired);

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage(ValidationMessages.StartDateRequired);

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage(ValidationMessages.EndDateRequired)
                .GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.EndDateGreaterThanStartDate);
        }
    }
}
