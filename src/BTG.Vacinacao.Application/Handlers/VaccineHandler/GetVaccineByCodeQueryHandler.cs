using BTG.Vacinacao.Application.DTOs;
using BTG.Vacinacao.Application.Queries.VaccineQuery;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace BTG.Vacinacao.Application.Handlers.VaccineHandler
{
    public class GetVaccineByCodeQueryHandler : IRequestHandler<GetVaccineByCodeQuery, VaccineDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetVaccineByCodeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VaccineDto> Handle(GetVaccineByCodeQuery request, CancellationToken cancellationToken)
        {
            var vaccine = await _unitOfWork.Vaccine.GetByCodeAsync(request.Code);

            if (vaccine is null)
                throw new ValidationException("Vaccine not found.");

            return new VaccineDto
            {
                Id = vaccine.Id,
                Name = vaccine.Name,
                Code = vaccine.Code
            };
        }
    }
}
