using BTG.Vacinacao.Application.DTOs.Person;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Commands.PersonCommand
{
    public record RegisterPersonCommand(string Name, string Cpf) : IRequest<PersonDto>;
    
}
