using EcoFarm.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.ServicePackageFeatures.Commands.CreateService
{
    public class CreateServiceCommand : IRequest<CreateServiceResponse>
    {
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string SupportAccountId { get; set; }
        public DateTime? StartDate { get; set; }
    }

    public class CreateServiceResponse
    {
    }

    internal class CreateServiceHandler : IRequestHandler<CreateServiceCommand, CreateServiceResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly 
        public CreateServiceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<CreateServiceResponse> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}