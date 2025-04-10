using BTG.Vacinacao.Core.Enums;

namespace BTG.Vacinacao.Application.DTOs.Vaccination
{
    public class VaccinationRecordDto
    {
        public Guid Id { get; set; }
        public string VaccineName { get; set; }
        public string DoseType { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}