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

        public async Task<bool> ExistsAsync(Guid personId, Guid vaccineId, DoseType doseType) =>
            await _dbSet.AnyAsync(v =>
                v.PersonId == personId &&
                v.VaccineId == vaccineId &&
                v.DoseType == doseType
            );

        public async Task<List<Vaccination>> GetByPersonIdWithVaccineAsync(Guid personId )=> 
            await _dbSet
                .Include(v => v.Vaccine)
                .Where(v => v.PersonId == personId)
                .ToListAsync();

        public async Task<List<Vaccination>> GetByPersonIdAsync(Guid personId) =>
            await _dbSet
                .Where(v => v.PersonId == personId)
                .ToListAsync();

        public void RemoveRange(IEnumerable<Vaccination> vaccinations) => 
            _dbSet.RemoveRange(vaccinations);
    }
}
