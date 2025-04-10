using BTG.Vacinacao.Application.Queries.VaccineQuery;
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
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .Matches(@"^\d{6}$").WithMessage("Code must be a 6-digit numeric value.");
        }
    }
}
