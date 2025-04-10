using BTG.Vacinacao.Application.Queries.VaccinationCardQuery;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Validators.VaccinationCardValidator
{
    public class GetVaccinationCardByCpfQueryValidator : AbstractValidator<GetVaccinationCardByCpfQuery>
    {
        public GetVaccinationCardByCpfQueryValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("CPF is required.")
                .Length(11).WithMessage("CPF must be exactly 11 characters.")
                .Matches(@"^\d{11}$").WithMessage("CPF must contain only numeric characters.");
        }
    }
}
