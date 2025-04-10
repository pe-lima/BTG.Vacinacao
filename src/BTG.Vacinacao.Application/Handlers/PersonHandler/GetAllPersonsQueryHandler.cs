using BTG.Vacinacao.Application.DTOs;
using BTG.Vacinacao.Application.Queries.PersonQuery;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using MediatR;

namespace BTG.Vacinacao.Application.Handlers.PersonHandler
{
    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, List<PersonDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllPersonsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<PersonDto>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            var persons = await _unitOfWork.Person.GetAllAsync();
            return persons.Select(p => new PersonDto
            {
                Id = p.Id,
                Name = p.Name,
                Cpf = p.Cpf
            }).ToList();
        }
    }
}
