using BTG.Vacinacao.Application.Queries.VaccineQuery;
using BTG.Vacinacao.Application.Validators.VaccineValidator;
using FluentValidation.TestHelper;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Validators.VaccineValidator
{
    public class GetVaccineByCodeQueryValidatorTests
    {
        private readonly GetVaccineByCodeQueryValidator _validator;

        public GetVaccineByCodeQueryValidatorTests()
        {
            _validator = new GetVaccineByCodeQueryValidator();
        }

        [Fact]
        public void Should_Pass_When_Code_Is_Valid()
        {
            var query = new GetVaccineByCodeQuery("123456");
            var result = _validator.TestValidate(query);

            result.ShouldNotHaveValidationErrorFor(x => x.Code);
        }

        [Theory]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("ABC123")]
        [InlineData("1234567")]
        [InlineData("12-456")]
        public void Should_Fail_When_Code_Is_Invalid(string invalidCode)
        {
            var query = new GetVaccineByCodeQuery(invalidCode);
            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(x => x.Code);
        }
    }
}
