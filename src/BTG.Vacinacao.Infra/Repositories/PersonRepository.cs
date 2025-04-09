using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Infra.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public Task AddAsync(Person person)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
