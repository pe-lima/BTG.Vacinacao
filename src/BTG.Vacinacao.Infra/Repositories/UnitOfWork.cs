using BTG.Vacinacao.Core.Interfaces.Repositories;
using BTG.Vacinacao.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Person = new PersonRepository(_context);
            Vaccine = new VaccineRepository(_context);
            Vaccination = new VaccinationRepository(_context);
        }

        public IPersonRepository Person { get; }
        public IVaccineRepository Vaccine { get; }
        public IVaccinationRepository Vaccination { get; }

        public Task<int> CommitAsync()
            => _context.SaveChangesAsync();
    }
}
