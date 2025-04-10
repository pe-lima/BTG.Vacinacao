using BTG.Vacinacao.Application.Commands.PersonCommand;
using FluentValidation;

namespace BTG.Vacinacao.Application.Validators.PersonValidator
{
    public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        public DeletePersonCommandValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("Cpf is required.")
                .Length(11).WithMessage("Cpf must be exactly 11 digits.")
                .Matches(@"^\d{11}$").WithMessage("Cpf must contain only digits.");
        }
    }
}
