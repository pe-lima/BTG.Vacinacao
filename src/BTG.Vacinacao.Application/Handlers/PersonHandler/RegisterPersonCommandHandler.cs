using BTG.Vacinacao.Application.Commands.PersonCommand;
using BTG.Vacinacao.Application.DTOs;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Handlers.PersonHandler
{
    public class RegisterPersonCommandHandler(IPersonRepository personRepository) : IRequestHandler<RegisterPersonCommand, PersonDto>
    {
        private readonly IPersonRepository _personRepository = personRepository;

        public async Task<PersonDto> Handle(RegisterPersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person(request.Name, request.Cpf);

            await _personRepository.AddAsync(person);

            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                Cpf = person.Cpf
            };
        }
    }
}
