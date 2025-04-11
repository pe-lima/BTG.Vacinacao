namespace BTG.Vacinacao.CrossCutting.Utils
{
    public static class CpfUtils
    {
        public static bool IsValid(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;
            if (cpf.Length != 11 || !cpf.All(char.IsDigit)) return false;

            if (cpf.Distinct().Count() == 1) return false;

            var tempCpf = cpf[..9];
            var firstDigit = CalculateDigit(tempCpf, firstMultiplier: 10);
            var secondDigit = CalculateDigit(tempCpf + firstDigit, firstMultiplier: 11);

            var cpfCalculated = tempCpf + firstDigit + secondDigit;
            return cpf == cpfCalculated;
        }

        private static string CalculateDigit(string baseCpf, int firstMultiplier)
        {
            int sum = 0;

            for (int i = 0; i < baseCpf.Length; i++)
                sum += (baseCpf[i] - '0') * (firstMultiplier - i);

            var remainder = sum % 11;
            return remainder < 2 ? "0" : (11 - remainder).ToString();
        }
    }
}
