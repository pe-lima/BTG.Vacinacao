﻿using BTG.Vacinacao.Application.Commands.VaccineCommand;
using BTG.Vacinacao.CrossCutting.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Validators.VaccineValidator
{
    public class RegisterVaccineCommandValidator : AbstractValidator<RegisterVaccineCommand>
    {
        public RegisterVaccineCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters.")
                .MaximumLength(100).WithMessage("Name must be no more than 100 characters.");

            RuleFor(x => x.Code).ApplyVaccineCodeRules();
        }
    }
}