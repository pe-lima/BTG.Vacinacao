﻿using BTG.Vacinacao.Application.DTOs.Vaccine;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Queries.VaccineQuery
{
    public record GetAllVaccinesQuery : IRequest<List<VaccineDto>>;
}
