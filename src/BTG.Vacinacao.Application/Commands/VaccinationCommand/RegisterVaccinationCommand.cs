using BTG.Vacinacao.Application.DTOs.Vaccination;
using BTG.Vacinacao.Core.Enums;
using MediatR;

namespace BTG.Vacinacao.Application.Commands.VaccinationCommand
{
    public record RegisterVaccinationCommand(
        string Cpf,
        string VaccineCode,
        DoseType DoseType,
        DateTime ApplicationDate
    ) : IRequest<VaccinationDto>;
}