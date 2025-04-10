using BTG.Vacinacao.Application.DTOs.Vaccine;
using BTG.Vacinacao.Application.Queries.VaccineQuery;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using MediatR;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.Handlers.VaccineHandler
{
    public class GetAllVaccinesQueryHandler : IRequestHandler<GetAllVaccinesQuery, List<VaccineDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllVaccinesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<VaccineDto>> Handle(GetAllVaccinesQuery request, CancellationToken cancellationToken)
        {
            var vaccines = await _unitOfWork.Vaccine.GetAllAsync();

            return vaccines.Select(v => new VaccineDto
            {
                Id = v.Id,
                Name = v.Name,
                Code = v.Code
            }).ToList();
        }
    }
}
