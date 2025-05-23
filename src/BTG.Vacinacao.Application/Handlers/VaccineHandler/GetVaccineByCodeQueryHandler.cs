﻿using BTG.Vacinacao.Application.DTOs.Vaccine;
using BTG.Vacinacao.Application.Queries.VaccineQuery;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using BTG.Vacinacao.CrossCutting.Exceptions;
using FluentValidation;
using MediatR;
using System.Net;

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
                throw new GlobalException("Vaccine not found.", HttpStatusCode.NotFound);

            return new VaccineDto
            {
                Id = vaccine.Id,
                Name = vaccine.Name,
                Code = vaccine.Code
            };
        }
    }
}
