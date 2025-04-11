using BTG.Vacinacao.Application.Commands.PersonCommand;
using BTG.Vacinacao.CrossCutting.Extensions;
using FluentValidation;

namespace BTG.Vacinacao.Application.Validators.PersonValidator
{
    public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        public DeletePersonCommandValidator()
        {
            RuleFor(x => x.Cpf).ApplyCpfRules();
        }
    }
}
