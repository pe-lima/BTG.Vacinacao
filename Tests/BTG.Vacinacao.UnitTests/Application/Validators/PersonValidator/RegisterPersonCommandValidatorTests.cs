using BTG.Vacinacao.Application.Commands.PersonCommand;
using BTG.Vacinacao.Application.Validators.PersonValidator;
using FluentValidation.TestHelper;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Validators.PersonValidator
{
    public class RegisterPersonCommandValidatorTests
    {
        private readonly RegisterPersonCommandValidator _validator;

        public RegisterPersonCommandValidatorTests()
        {
            _validator = new RegisterPersonCommandValidator();
        }

        [Fact]
        public void Should_Pass_When_Name_And_Cpf_Are_Valid()
        {
            var command = new RegisterPersonCommand("João da Silva", "12345678909");

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("")]
        [InlineData("Jo")]
        [InlineData(null)]
        public void Should_Fail_When_Name_Is_Invalid(string invalidName)
        {
            var command = new RegisterPersonCommand(invalidName, "12345678909");

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Too_Long()
        {
            var command = new RegisterPersonCommand(new string('A', 101), "12345678909");

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData("12345")]
        [InlineData("123ABC456DE")]
        [InlineData("123.456.789-01")]
        public void Should_Fail_When_Cpf_Is_Invalid(string invalidCpf)
        {
            var command = new RegisterPersonCommand("João da Silva", invalidCpf);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Pass_When_Cpf_Is_Valid()
        {
            var command = new RegisterPersonCommand("João da Silva", "12345678909");

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.Cpf);
        }
    }
}
