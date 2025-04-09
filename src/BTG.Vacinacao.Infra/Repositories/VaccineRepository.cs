using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repository;
using BTG.Vacinacao.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Infra.Repositories
{
    public class VaccineRepository : IVaccineRepository
    {
        private readonly ApplicationDbContext _context;
        public VaccineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Vaccine vaccine)
        {
            await _context.Vaccines.AddAsync(vaccine);
            await _context.SaveChangesAsync();
        }

        public async Task<Vaccine> GetByIdAsync(Guid id)
        {
            return await _context.Vaccines.FirstAsync(p => p.Id == id);
        }
    }
}
