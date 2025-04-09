using BTG.Vacinacao.Application.Commands.VaccinationCommand;
using BTG.Vacinacao.Application.Validators.VaccinationValidator;
using BTG.Vacinacao.Core.Enums;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Validators.VaccinationValidator
{
    public class RegisterVaccinationCommandValidatorTests
    {
        private readonly RegisterVaccinationCommandValidator _validator;

        public RegisterVaccinationCommandValidatorTests()
        {
            _validator = new RegisterVaccinationCommandValidator();
        }

        [Fact]
        public void Should_Pass_When_Command_Is_Valid()
        {
            var command = new RegisterVaccinationCommand("12345678901", "123456", DoseType.FirstDose, DateTime.Today);

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("123")]
        [InlineData("123.456.789-00")]
        public void Should_Fail_When_Cpf_Is_Invalid(string invalidCpf)
        {
            var command = new RegisterVaccinationCommand(invalidCpf, "123456", DoseType.FirstDose, DateTime.Today);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Theory]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("ABC123")]
        [InlineData("1234567")]
        [InlineData("12-456")]
        public void Should_Fail_When_VaccineCode_Is_Invalid(string invalidCode)
        {
            var command = new RegisterVaccinationCommand("12345678901", invalidCode, DoseType.FirstDose, DateTime.Today);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.VaccineCode);
        }

        [Fact]
        public void Should_Fail_When_ApplicationDate_Is_In_The_Future()
        {
            var command = new RegisterVaccinationCommand("12345678901", "123456", DoseType.FirstDose, DateTime.Today.AddDays(1));

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.ApplicationDate);
        }

        [Fact]
        public void Should_Pass_With_Valid_ApplicationDate()
        {
            var command = new RegisterVaccinationCommand("12345678901", "123456", DoseType.FirstDose, DateTime.Today);

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.ApplicationDate);
        }
    }
}
