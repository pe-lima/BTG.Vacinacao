using BTG.Vacinacao.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Core.Interfaces.Repository
{
    public interface IVaccineRepository
    {
        Task AddAsync(Vaccine vaccine);
        Task<Vaccine> GetByIdAsync(Guid id);
    }
}
