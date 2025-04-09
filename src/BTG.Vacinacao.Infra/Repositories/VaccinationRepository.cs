using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Enums;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using BTG.Vacinacao.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace BTG.Vacinacao.Infra.Repositories
{
    public class VaccinationRepository : Repository<Vaccination>, IVaccinationRepository
    {
        public VaccinationRepository(ApplicationDbContext context) : base(context) { }

        public Task<bool> ExistsAsync(Guid personId, Guid vaccineId, DoseType doseType)
        {
            return _dbSet.AnyAsync(v =>
                v.PersonId == personId &&
                v.VaccineId == vaccineId &&
                v.DoseType == doseType
            );
        }
    }
}
