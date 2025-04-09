using BTG.Vacinacao.Application.Commands.VaccinationCommand;
using FluentValidation;
using System;

namespace BTG.Vacinacao.Application.Validators.VaccinationValidator
{
    public class RegisterVaccinationCommandValidator : AbstractValidator<RegisterVaccinationCommand>
    {
        public RegisterVaccinationCommandValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("Cpf is required.")
                .Length(11).WithMessage("Cpf must be exactly 11 characters.")
                .Matches(@"^\d{11}$").WithMessage("Cpf must contain only numeric characters.");

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
