using BTG.Vacinacao.Application.Queries.PersonQuery;
using BTG.Vacinacao.Application.Validators.PersonValidator;
using FluentValidation.TestHelper;

namespace BTG.Vacinacao.UnitTests.Application.Validators.PersonValidator
{
    public class GetPersonByCpfQueryValidatorTests
    {
        private readonly GetPersonByCpfQueryValidator _validator;

        public GetPersonByCpfQueryValidatorTests()
        {
            _validator = new GetPersonByCpfQueryValidator();
        }

        [Fact]
        public void Should_Pass_When_Cpf_Is_Valid()
        {
            var query = new GetPersonByCpfQuery("12345678909");
            var result = _validator.TestValidate(query);
            
            result.ShouldNotHaveValidationErrorFor(x => x.Cpf);
        }

        [Theory]
        [InlineData("")]
        [InlineData("123456789")]
        [InlineData("123456789012345")]
        [InlineData("1234567890a")]
        [InlineData("123-456-7890")]
        public void Should_Fail_When_Cpf_Is_Invalid(string invalidCpf)
        {
            var query = new GetPersonByCpfQuery(invalidCpf);
            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }
    }
}
