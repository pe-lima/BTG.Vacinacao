using BTG.Vacinacao.Core.Enums;

namespace BTG.Vacinacao.Application.DTOs.Vaccination
{
    public class VaccinationDto
    {
        public Guid Id { get; set; }
        public string PersonName { get; set; } = string.Empty;
        public string VaccineName { get; set; } = string.Empty;
        public DoseType DoseType { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
