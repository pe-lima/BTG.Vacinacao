using BTG.Vacinacao.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Commands.VaccineCommand
{
    public record RegisterVaccineCommand(string Name, string Code) : IRequest<VaccineDto>;
}
