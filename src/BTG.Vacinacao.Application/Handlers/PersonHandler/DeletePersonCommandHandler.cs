using BTG.Vacinacao.Application.Commands.PersonCommand;
using BTG.Vacinacao.CrossCutting.Exceptions;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using System.Net;

namespace BTG.Vacinacao.Application.Handlers.PersonHandler
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePersonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.Person.GetByCpfAsync(request.Cpf);

            if (person is null)
                throw new GlobalException("Person not found.", HttpStatusCode.NotFound);

            var vaccinations = await _unitOfWork.Vaccination.GetByPersonIdAsync(person.Id);

            if (vaccinations.Count != 0)
                _unitOfWork.Vaccination.RemoveRange(vaccinations);

            _unitOfWork.Person.Remove(person);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
