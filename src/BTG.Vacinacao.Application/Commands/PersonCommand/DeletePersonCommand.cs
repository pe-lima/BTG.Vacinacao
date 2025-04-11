using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Commands.PersonCommand
{
    public record DeletePersonCommand(string Cpf) : IRequest<Unit>;
}
