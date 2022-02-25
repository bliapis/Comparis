using FluentValidation;

namespace Comparis.Application.UseCases.Payment.Commands.ProccessPayment
{
    public class ProccessPaymentCommandValidator : AbstractValidator<ProccessPaymentCommand>
    {
        public ProccessPaymentCommandValidator()
        {
            RuleFor(p => p.Amount)
                .NotEmpty().WithMessage("{Amount} is required.")
                .GreaterThan(0).WithMessage("{Amount} must be greater than zero.");
        }
    }
}