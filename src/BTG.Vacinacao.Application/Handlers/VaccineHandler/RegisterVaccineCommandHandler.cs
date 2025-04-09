using BTG.Vacinacao.Application.Commands.VaccineCommand;
using BTG.Vacinacao.Application.DTOs;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repository;
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
        private readonly IVaccineRepository _vaccineRepository;
        public RegisterVaccineCommandHandler(IVaccineRepository vaccineRepository)
        {
            _vaccineRepository = vaccineRepository;
        }

        public async Task<VaccineDto> Handle(RegisterVaccineCommand request, CancellationToken cancellationToken)
        {
            var vaccine = new Vaccine(request.Name, request.Code);

            await _vaccineRepository.AddAsync(vaccine);

            return new VaccineDto
            {
                Id = vaccine.Id,
                Name = vaccine.Name,
                Code = vaccine.Code
            };
        }
    }
}
