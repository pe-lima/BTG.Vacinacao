using BTG.Vacinacao.Application.Commands.PersonCommand;
using BTG.Vacinacao.Application.DTOs.Person;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.CrossCutting.Exceptions;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace BTG.Vacinacao.Application.Handlers.PersonHandler
{
    public class RegisterPersonCommandHandler : IRequestHandler<RegisterPersonCommand, PersonDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterPersonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PersonDto> Handle(RegisterPersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person(request.Name, request.Cpf);

            if (await _unitOfWork.Person.ExistsByCpfAsync(request.Cpf))
                throw new GlobalException("CPF already exists.", HttpStatusCode.Conflict);


            await _unitOfWork.Person.AddAsync(person);
            await _unitOfWork.CommitAsync();

            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                Cpf = person.Cpf
            };
        }
    }
}
