using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IPersonRepository Person { get; }
        IVaccineRepository Vaccine { get; }
        IVaccinationRepository Vaccination { get; }
        IUserRepository User { get; }

        Task<int> CommitAsync();

    }
}
