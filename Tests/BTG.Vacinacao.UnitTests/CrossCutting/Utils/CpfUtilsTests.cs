using BTG.Vacinacao.CrossCutting.Utils;

namespace BTG.Vacinacao.UnitTests.CrossCutting.Utils
{
    public class CpfUtilsTests
    {
        [Theory]
        [InlineData("12345678909")]
        [InlineData("11144477735")]
        [InlineData("52998224725")]
        public void Should_Return_True_When_Cpf_Is_Valid(string cpf)
        {
            var result = CpfUtils.IsValid(cpf);
            Assert.True(result);
        }

        [Theory]
        [InlineData("12345678900")] 
        [InlineData("11111111111")] 
        [InlineData("00000000000")]
        [InlineData("abcdefghijk")] 
        [InlineData("1234567890")]  
        [InlineData("123456789012")]
        [InlineData("")]            
        [InlineData("           ")] 
        public void Should_Return_False_When_Cpf_Is_Invalid(string cpf)
        {
            var result = CpfUtils.IsValid(cpf);
            Assert.False(result);
        }
    }
}
