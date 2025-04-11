using BTG.Vacinacao.Application.Queries.PersonQuery;
using BTG.Vacinacao.CrossCutting.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Validators.PersonValidator
{
    public class GetPersonByCpfQueryValidator : AbstractValidator<GetPersonByCpfQuery>
    {
        public GetPersonByCpfQueryValidator()
        {
            RuleFor(x => x.Cpf).ApplyCpfRules();
        }
    }
}
