using BTG.Vacinacao.Application.Queries.PersonQuery;
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
            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("CPF is required.")
                .Length(11).WithMessage("Cpf must be exactly 11 characters.")
                .Matches(@"^\d{11}$").WithMessage("Cpf must contain only numeric characters.");
        }
    }
}
