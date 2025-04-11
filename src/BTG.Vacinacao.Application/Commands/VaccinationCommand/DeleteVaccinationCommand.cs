using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Commands.VaccinationCommand
{
    public record DeleteVaccinationCommand(Guid VaccinationId) : IRequest<Unit>;
}
