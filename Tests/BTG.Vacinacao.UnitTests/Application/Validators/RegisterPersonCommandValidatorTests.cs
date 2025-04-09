using BTG.Vacinacao.Application.Commands.Person;
using BTG.Vacinacao.Application.Validators.PersonValidator;
using FluentValidation.TestHelper;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Validators
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
            var command = new RegisterPersonCommand("João da Silva");
            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Empty()
        {
            var command = new RegisterPersonCommand("");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Too_Short()
        {
            var command = new RegisterPersonCommand("Jo");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Too_Long()
        {
            var command = new RegisterPersonCommand(new string('A', 101));
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}
