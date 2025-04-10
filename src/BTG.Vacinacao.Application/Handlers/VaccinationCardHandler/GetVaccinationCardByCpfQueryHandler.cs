using BTG.Vacinacao.Application.DTOs;
using BTG.Vacinacao.Application.DTOs.Vaccination;
using BTG.Vacinacao.Application.Queries.VaccinationCardQuery;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using BTG.Vacinacao.CrossCutting.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Handlers.VaccinationCardHandler
{
    public class GetVaccinationCardByCpfQueryHandler : IRequestHandler<GetVaccinationCardByCpfQuery, VaccinationCardDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetVaccinationCardByCpfQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VaccinationCardDto> Handle(GetVaccinationCardByCpfQuery request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.Person.GetByCpfAsync(request.Cpf);
            if (person == null)
                throw new GlobalException("Person not found.", HttpStatusCode.NotFound);

            var vaccinations = await _unitOfWork.Vaccination.GetByPersonIdWithVaccineAsync(person.Id);
            var vaccinationCard = new VaccinationCardDto
            {
                Name = person.Name,
                Cpf = person.Cpf,
                Vaccinations = vaccinations.Select(v => new VaccinationRecordDto 
                {
                    Id = v.Id,
                    VaccineName = v.Vaccine.Name,
                    DoseType = v.DoseType.ToString(),
                    ApplicationDate = v.ApplicationDate
                }).ToList()
            };

            return vaccinationCard;
        }
    }
}
