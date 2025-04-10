using BTG.Vacinacao.Application.Commands.VaccinationCommand;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace BTG.Vacinacao.Application.Handlers.VaccinationHandler
{
    public class DeleteVaccinationCommandHandler : IRequestHandler<DeleteVaccinationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVaccinationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteVaccinationCommand request, CancellationToken cancellationToken)
        {
            var vaccination = await _unitOfWork.Vaccination.GetByIdAsync(request.VaccinationId);

            if (vaccination is null)
                throw new ValidationException("Vaccination record not found.");

            _unitOfWork.Vaccination.Remove(vaccination);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}

