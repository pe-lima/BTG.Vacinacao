using BTG.Vacinacao.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Core.Interfaces.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<bool> ExistsByCpfAsync(string cpf);
        Task<Person?> GetByCpfAsync(string cpf);
    }
}
