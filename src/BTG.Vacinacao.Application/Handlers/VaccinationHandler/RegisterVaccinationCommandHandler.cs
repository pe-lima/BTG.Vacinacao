using BTG.Vacinacao.Application.Commands.VaccinationCommand;
using BTG.Vacinacao.Application.DTOs;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Enums;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using FluentValidation;
using MediatR;

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
                throw new ValidationException("Person not found.");

            var vaccine = await _unitOfWork.Vaccine.GetByCodeAsync(request.VaccineCode);
            if (vaccine is null)
                throw new ValidationException("Vaccine not found.");

            var exists = await _unitOfWork.Vaccination.ExistsAsync(person.Id, vaccine.Id, request.DoseType);
            if (exists)
                throw new ValidationException("This dose has already been registered for this person and vaccine.");

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
