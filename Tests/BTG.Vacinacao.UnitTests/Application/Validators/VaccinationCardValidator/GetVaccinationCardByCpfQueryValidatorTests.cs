using BTG.Vacinacao.Application.Queries.VaccinationCardQuery;
using BTG.Vacinacao.Application.Validators.VaccinationCardValidator;
using FluentValidation.TestHelper;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Validators.VaccinationCardValidator
{
    public class GetVaccinationCardByCpfQueryValidatorTests
    {
        private readonly GetVaccinationCardByCpfQueryValidator _validator;

        public GetVaccinationCardByCpfQueryValidatorTests()
        {
            _validator = new GetVaccinationCardByCpfQueryValidator();
        }

        [Fact]
        public void Should_Pass_When_Cpf_Is_Valid()
        {
            var query = new GetVaccinationCardByCpfQuery("12345678901");

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveValidationErrorFor(x => x.Cpf);
        }

        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("123")]
        [InlineData("123.456.789-00")]
        [InlineData("1234567890A")]
        public void Should_Fail_When_Cpf_Is_Invalid(string invalidCpf)
        {
            var query = new GetVaccinationCardByCpfQuery(invalidCpf);

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }
    }
}
