using BTG.Vacinacao.Application.Commands.VaccinationCommand;
using BTG.Vacinacao.CrossCutting.Extensions;
using FluentValidation;
using System;

namespace BTG.Vacinacao.Application.Validators.VaccinationValidator
{
    public class RegisterVaccinationCommandValidator : AbstractValidator<RegisterVaccinationCommand>
    {
        public RegisterVaccinationCommandValidator()
        {
            RuleFor(x => x.Cpf).ApplyCpfRules();

            RuleFor(x => x.VaccineCode)
                .NotEmpty().WithMessage("Vaccine code is required.")
                .Matches(@"^\d{6}$").WithMessage("Vaccine code must be a 6-digit numeric value.");

            RuleFor(x => x.ApplicationDate)
                .NotEmpty().WithMessage("Application date is required.")
                .Must(date => date <= DateTime.Today)
                .WithMessage("Application date cannot be in the future.");
        }
    }
}
