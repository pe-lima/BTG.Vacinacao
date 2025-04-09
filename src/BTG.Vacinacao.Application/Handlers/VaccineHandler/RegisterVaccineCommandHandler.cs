using BTG.Vacinacao.Application.Commands.VaccineCommand;
using BTG.Vacinacao.Application.DTOs;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Handlers.VaccineHandler
{
    public class RegisterVaccineCommandHandler : IRequestHandler<RegisterVaccineCommand, VaccineDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterVaccineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VaccineDto> Handle(RegisterVaccineCommand request, CancellationToken cancellationToken)
        {
            var vaccine = new Vaccine(request.Name, request.Code);

            if (await _unitOfWork.Vaccine.ExistsByCodeAsync(request.Code))
                throw new ValidationException("Vaccine code already exists.");

            await _unitOfWork.Vaccine.AddAsync(vaccine);
            await _unitOfWork.CommitAsync();

            return new VaccineDto
            {
                Id = vaccine.Id,
                Name = vaccine.Name,
                Code = vaccine.Code
            };
        }
    }
}
