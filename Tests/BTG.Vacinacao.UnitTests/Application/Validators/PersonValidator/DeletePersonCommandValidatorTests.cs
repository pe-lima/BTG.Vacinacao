using BTG.Vacinacao.Application.Commands.PersonCommand;
using BTG.Vacinacao.Application.Validators.PersonValidator;
using FluentValidation.TestHelper;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Validators.PersonValidator
{
    public class DeletePersonCommandValidatorTests
    {
        private readonly DeletePersonCommandValidator _validator;

        public DeletePersonCommandValidatorTests()
        {
            _validator = new DeletePersonCommandValidator();
        }

        [Fact]
        public void Should_Pass_When_Cpf_Is_Valid()
        {
            var command = new DeletePersonCommand("12345678901");
            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.Cpf);
        }

        [Theory]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("abc45678901")]
        [InlineData("123.456.789-00")]
        public void Should_Fail_When_Cpf_Is_Invalid(string invalidCpf)
        {
            var command = new DeletePersonCommand(invalidCpf);
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }
    }
}
