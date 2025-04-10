using BTG.Vacinacao.Application.DTOs;
using BTG.Vacinacao.Application.Queries.PersonQuery;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Handlers.PersonHandler
{
    public class GetPersonByCpfQueryHandler : IRequestHandler<GetPersonByCpfQuery, PersonDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPersonByCpfQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PersonDto> Handle(GetPersonByCpfQuery request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.Person.GetByCpfAsync(request.Cpf);

            if (person is null)
                throw new ValidationException("Person not found.");

            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                Cpf = person.Cpf
            };
        }
    }
}
