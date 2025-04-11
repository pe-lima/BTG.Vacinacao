using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Core.Interfaces.Repositories
{
    public interface IVaccinationRepository : IRepository<Vaccination>
    {
        Task<bool> ExistsAsync(Guid personId, Guid vaccineId, DoseType doseType);

        Task<List<Vaccination>> GetByPersonIdWithVaccineAsync(Guid personId);

        Task<List<Vaccination>> GetByPersonIdAsync(Guid personId);

        void RemoveRange(IEnumerable<Vaccination> vaccinations);
    }
}
