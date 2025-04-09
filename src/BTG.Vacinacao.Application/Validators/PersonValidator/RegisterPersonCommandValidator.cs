using BTG.Vacinacao.Application.Commands.PersonCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Validators.PersonValidator
{
    public class RegisterPersonCommandValidator : AbstractValidator<RegisterPersonCommand>
    {
        public RegisterPersonCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters.")
                .MaximumLength(100).WithMessage("Name must be no more than 100 characters.");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("Cpf is required.")
                .Length(11).WithMessage("Cpf must be exactly 11 characters.")
                .Matches(@"^\d{11}$").WithMessage("Cpf must contain only numeric characters.");
        }
    }
}
