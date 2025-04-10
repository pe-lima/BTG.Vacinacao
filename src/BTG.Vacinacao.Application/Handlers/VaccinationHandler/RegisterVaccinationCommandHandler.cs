using BTG.Vacinacao.Application.Commands.VaccinationCommand;
using BTG.Vacinacao.Application.DTOs.Vaccination;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using BTG.Vacinacao.CrossCutting.Exceptions;
using MediatR;
using System.Net;

namespace BTG.Vacinacao.Application.Handlers.VaccinationHandler
{
    public class RegisterVaccinationCommandHandler(IUnitOfWork unitOfWork) 
        : IRequestHandler<RegisterVaccinationCommand, VaccinationDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<VaccinationDto> Handle(RegisterVaccinationCommand request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.Person.GetByCpfAsync(request.Cpf);
            if (person is null)
                throw new GlobalException("Person not found.", HttpStatusCode.NotFound);

            var vaccine = await _unitOfWork.Vaccine.GetByCodeAsync(request.VaccineCode);
            if (vaccine is null)
                throw new GlobalException("Vaccine not found.", HttpStatusCode.NotFound);

            var exists = await _unitOfWork.Vaccination.ExistsAsync(person.Id, vaccine.Id, request.DoseType);
            if (exists)
                throw new GlobalException("This dose has already been registered for this person and vaccine.", HttpStatusCode.Conflict);

            var vaccination = new Vaccination(person.Id, vaccine.Id, request.DoseType, request.ApplicationDate);

            await _unitOfWork.Vaccination.AddAsync(vaccination);
            await _unitOfWork.CommitAsync();

            return new VaccinationDto
            {
                Id = vaccination.Id,
                PersonName = person.Name,
                VaccineName = vaccine.Name,
                DoseType = vaccination.DoseType,
                ApplicationDate = vaccination.ApplicationDate
            };
        }
    }
}
