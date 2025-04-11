using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using BTG.Vacinacao.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Infra.Repositories
{
    public class VaccineRepository : Repository<Vaccine>, IVaccineRepository
    {
        public VaccineRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> ExistsByCodeAsync(string code) =>
            await _dbSet.AnyAsync(v => v.Code == code);

        public async Task<Vaccine?> GetByCodeAsync(string code) =>
            await _dbSet.FirstOrDefaultAsync(v => v.Code == code);
    }
}
