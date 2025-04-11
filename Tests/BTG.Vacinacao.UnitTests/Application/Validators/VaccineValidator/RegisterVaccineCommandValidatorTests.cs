using BTG.Vacinacao.Application.Commands.VaccineCommand;
using BTG.Vacinacao.Application.Validators.VaccineValidator;
using FluentValidation.TestHelper;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Validators.VaccineValidator
{
    public class RegisterVaccineCommandValidatorTests
    {
        private readonly RegisterVaccineCommandValidator _validator;

        public RegisterVaccineCommandValidatorTests()
        {
            _validator = new RegisterVaccineCommandValidator();
        }

        [Fact]
        public void Should_Pass_When_Name_And_Code_Are_Valid()
        {
            var command = new RegisterVaccineCommand("Covid-19", "123456");

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("")]
        [InlineData("co")]
        [InlineData(null)]
        public void Should_Fail_When_Name_Is_Invalid(string invalidName)
        {
            var command = new RegisterVaccineCommand(invalidName, "123456");

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Too_Long()
        {
            var command = new RegisterVaccineCommand(new string('C', 101), "123456");

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("1234567")]
        [InlineData("ABC123")]
        [InlineData("12-456")]
        public void Should_Fail_When_Code_Is_Invalid(string invalidCode)
        {
            var command = new RegisterVaccineCommand("Covid-19", invalidCode);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Should_Pass_When_Code_Is_Valid()
        {
            var command = new RegisterVaccineCommand("Covid-19", "123456");

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.Code);
        }
    }
}
