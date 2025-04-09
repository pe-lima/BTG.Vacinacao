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

        public Task<bool> ExistsByCodeAsync(string code) =>
            _dbSet.AnyAsync(v => v.Code == code);

        public Task<Vaccine?> GetByCodeAsync(string code) =>
            _dbSet.FirstOrDefaultAsync(v => v.Code == code);
    }
}
