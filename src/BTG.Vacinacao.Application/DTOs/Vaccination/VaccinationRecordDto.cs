using BTG.Vacinacao.Core.Enums;

namespace BTG.Vacinacao.Application.DTOs.Vaccination
{
    public class VaccinationRecordDto
    {
        public Guid Id { get; set; }
        public string VaccineName { get; set; } = string.Empty;
        public string DoseType { get; set; } = string.Empty;    
        public DateTime ApplicationDate { get; set; }
    }
}