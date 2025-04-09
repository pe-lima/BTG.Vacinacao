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
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext context) : base(context) { }

        public Task<bool> ExistsByCpfAsync(string cpf) =>
            _dbSet.AnyAsync(p => p.Cpf == cpf);

        public Task<Person?> GetByCpfAsync(string cpf) =>
            _dbSet.FirstOrDefaultAsync(p => p.Cpf == cpf);
    }
}
