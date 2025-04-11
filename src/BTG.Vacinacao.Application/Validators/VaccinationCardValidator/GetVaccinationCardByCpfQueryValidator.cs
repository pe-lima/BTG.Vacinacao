using BTG.Vacinacao.Application.Queries.VaccinationCardQuery;
using BTG.Vacinacao.CrossCutting.Extensions;
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
            RuleFor(x => x.Cpf).ApplyCpfRules();
        }
    }
}
