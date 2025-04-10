using BTG.Vacinacao.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Queries.VaccinationCardQuery
{
    public record GetVaccinationCardByCpfQuery(string Cpf) : IRequest<VaccinationCardDto>;

}
