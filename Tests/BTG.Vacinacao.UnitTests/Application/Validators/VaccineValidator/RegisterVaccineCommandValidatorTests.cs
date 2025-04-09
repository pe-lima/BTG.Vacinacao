using BTG.Vacinacao.Application.Commands.VaccineCommand;
using BTG.Vacinacao.Application.Validators.VaccineValidator;
using FluentValidation.TestHelper;

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

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Empty()
        {
            var command = new RegisterVaccineCommand("", "123456");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Too_Short()
        {
            var command = new RegisterVaccineCommand("co", "123456");
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

        [Fact]
        public void Should_Fail_When_Code_Is_Empty()
        {
            var command = new RegisterVaccineCommand("Covid-19", "");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Should_Fail_When_Code_Is_Short()
        {
            var command = new RegisterVaccineCommand("Covid-19", "123");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Should_Fail_When_Code_Is_Too_Long()
        {
            var command = new RegisterVaccineCommand("Covid-19", "1234567");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Should_Fail_When_Code_Has_Letters()
        {
            var command = new RegisterVaccineCommand("Covid-19", "ABC123");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Should_Fail_When_Code_Has_SpecialCharacters()
        {
            var command = new RegisterVaccineCommand("Covid-19", "12-456");
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Code);
        }
    }
}
