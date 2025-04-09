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
        public void Should_Pass_When_Name_Is_Valid()
        {
            var command = new RegisterPersonCommand("João da Silva", "12312312312");
            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Empty()
        {
            var command = new RegisterPersonCommand("", "12312312312");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Too_Short()
        {
            var command = new RegisterPersonCommand("Jo", "12312312312");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Too_Long()
        {
            var command = new RegisterPersonCommand(new string('A', 101), "12312312312");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Pass_When_Cpf_Is_Valid()
        {
            var command = new RegisterPersonCommand("João da Silva", "12345678901");
            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Fail_When_Cpf_Is_Empty()
        {
            var command = new RegisterPersonCommand("João da Silva", "");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Fail_When_Cpf_Is_Short()
        {
            var command = new RegisterPersonCommand("João da Silva", "12345");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Fail_When_Cpf_Has_Letters()
        {
            var command = new RegisterPersonCommand("João da Silva", "123ABC456DE");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Fail_When_Cpf_Has_Special_Characters()
        {
            var command = new RegisterPersonCommand("João da Silva", "123.456.789-01");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

    }
}
