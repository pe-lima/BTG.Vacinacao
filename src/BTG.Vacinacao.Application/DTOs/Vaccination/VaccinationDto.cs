using BTG.Vacinacao.Core.Enums;

namespace BTG.Vacinacao.Application.DTOs
{
    public class VaccinationDto
    {
        public Guid Id { get; set; }
        public string PersonName { get; set; }
        public string VaccineName { get; set; }
        public DoseType DoseType { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
