using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.DTOs.Vaccination
{
    public class VaccinationCardDto
    {
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public List<VaccinationRecordDto> Vaccinations { get; set; } = new List<VaccinationRecordDto>();
    }
}
