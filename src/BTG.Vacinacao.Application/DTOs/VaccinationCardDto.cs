using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.DTOs
{
    public class VaccinationCardDto
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public List<VaccinationRecordDto> Vaccinations { get; set; } = new List<VaccinationRecordDto>();
    }
}
