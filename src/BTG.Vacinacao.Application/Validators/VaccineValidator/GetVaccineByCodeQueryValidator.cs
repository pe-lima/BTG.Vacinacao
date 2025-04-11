using BTG.Vacinacao.Application.Queries.VaccineQuery;
using BTG.Vacinacao.CrossCutting.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Validators.VaccineValidator
{
    public class GetVaccineByCodeQueryValidator : AbstractValidator<GetVaccineByCodeQuery>
    {
        public GetVaccineByCodeQueryValidator()
        {
            RuleFor(x => x.Code).ApplyVaccineCodeRules();
        }
    }
}
