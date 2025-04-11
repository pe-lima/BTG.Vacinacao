using BTG.Vacinacao.Application.Commands.VaccinationCommand;
using BTG.Vacinacao.Application.Validators.VaccinationValidator;
using FluentValidation.TestHelper;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Validators.VaccinationValidator
{
    public class DeleteVaccinationCommandValidatorTests
    {
        private readonly DeleteVaccinationCommandValidator _validator;

        public DeleteVaccinationCommandValidatorTests()
        {
            _validator = new DeleteVaccinationCommandValidator();
        }

        [Fact]
        public void Should_Pass_When_Id_Is_Valid()
        {
            var command = new DeleteVaccinationCommand(Guid.NewGuid());
            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.VaccinationId);
        }

        [Fact]
        public void Should_Fail_When_Id_Is_Empty()
        {
            var command = new DeleteVaccinationCommand(Guid.Empty);
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.VaccinationId);
        }
    }
}
