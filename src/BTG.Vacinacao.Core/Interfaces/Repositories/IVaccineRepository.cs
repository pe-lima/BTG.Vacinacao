using BTG.Vacinacao.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Core.Interfaces.Repositories
{
    public interface IVaccineRepository : IRepository<Vaccine>
    {
        Task<bool> ExistsByCodeAsync(string code);
        Task<Vaccine?> GetByCodeAsync(string code);
    }
}
