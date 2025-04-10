using BTG.Vacinacao.Application.Commands.VaccinationCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Validators.VaccinationValidator
{
    public class DeleteVaccinationCommandValidator : AbstractValidator<DeleteVaccinationCommand>
    {
        public DeleteVaccinationCommandValidator()
        {
            RuleFor(x => x.VaccinationId)
                .NotEmpty().WithMessage("Vaccination ID is required.");
        }
    }
}
