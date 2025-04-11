using BTG.Vacinacao.CrossCutting.Utils;
using FluentValidation;

namespace BTG.Vacinacao.CrossCutting.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> ApplyCpfRules<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Cpf is required.")
                .Length(11).WithMessage("Cpf must be exactly 11 characters.")
                .Matches(@"^\d{11}$").WithMessage("Cpf must contain only numeric characters.")
                .Must(CpfUtils.IsValid).WithMessage("CPF is incorrect.");
        }

        public static IRuleBuilderOptions<T, string> ApplyVaccineCodeRules<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Code is required.")
                .Matches(@"^\d{6}$").WithMessage("Code must be a 6-digit numeric value.");
        }
    }
}
