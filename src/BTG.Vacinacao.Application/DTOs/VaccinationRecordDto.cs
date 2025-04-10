using BTG.Vacinacao.Core.Enums;

namespace BTG.Vacinacao.Application.DTOs
{
    public class VaccinationRecordDto
    {
        public string VaccineName { get; set; }
        public string DoseType { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}