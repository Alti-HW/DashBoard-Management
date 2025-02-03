using Dashboard_Management.Constants;
using Dashboard_Management.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace Dashboard_Management.Validators.Energy
{
    public class EnergyConsumptionRequestValidator : AbstractValidator<EnergyConsumptionRequestDto>
    {
        public EnergyConsumptionRequestValidator()
        {
            RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage(ValidationMessages.StartDateRequired);
            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.EndDateGreaterThanStartDate);
        }
    }
}
