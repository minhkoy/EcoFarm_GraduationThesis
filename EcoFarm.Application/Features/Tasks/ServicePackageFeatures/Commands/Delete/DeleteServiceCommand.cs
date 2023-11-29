using EcoFarm.Application.Common.Results;
using EcoFarm.Application.Interfaces.Messagings_Prev;
using EcoFarm.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.Tasks.ServicePackageFeatures.Commands.Delete
{
    public class DeleteServiceCommand : ICommand<bool>
    {
        public string Id { get; set; }
    }

    internal class DeleteServiceCommandHandler : ICommandHandler<DeleteServiceCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.FarmingPackages.FindAsync(request.Id);
            if (service == null)
                return new BadRequestResult<bool>("Không tìm thấy dịch vụ", Enumerable.Empty<object>());
            if (service.IS_DELETE)
                return new BadRequestResult<bool>("Dịch vụ đã bị xóa", Enumerable.Empty<object>());
            service.IS_DELETE = true;
            _unitOfWork.FarmingPackages.Remove(service);
            await _unitOfWork.SaveChangesAsync();
            return new OkResult<bool>(true);
        }
    }
}
