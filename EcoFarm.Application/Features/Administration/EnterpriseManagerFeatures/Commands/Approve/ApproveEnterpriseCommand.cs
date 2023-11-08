using EcoFarm.Application.Common.Results;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.Administration.EnterpriseManagerFeatures.Commands.Approve
{
    public class ApproveEnterpriseCommand : ICommand<bool>
    {
        public string EnterpriseId { get; set; }
    }

    internal class ApproveEnterpriseCommandHandler : ICommandHandler<ApproveEnterpriseCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApproveEnterpriseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ApproveEnterpriseCommand request, CancellationToken cancellationToken)
        {
            var enterprise = await _unitOfWork.SellerEnterprises.FindAsync(request.EnterpriseId);
            if (enterprise == null)
                return new BadRequestResult<bool>("Không tìm thấy doanh nghiệp", Enumerable.Empty<object>());
            if (enterprise.IS_DELETE)
                return new BadRequestResult<bool>("Doanh nghiệp đã bị xóa", Enumerable.Empty<object>());
            if (!enterprise.IS_APPROVED.HasValue)
            {
                if (enterprise.IS_APPROVED.Value)
                    return new BadRequestResult<bool>("Doanh nghiệp đã được duyệt", Enumerable.Empty<object>());
                else 
                    return new BadRequestResult<bool>("Doanh nghiệp đã bị từ chối", Enumerable.Empty<object>());
            }            
            enterprise.IS_APPROVED = true;
            _unitOfWork.SellerEnterprises.Update(enterprise);
            await _unitOfWork.SaveChangesAsync();
            return new OkResult<bool>(true);
        }
    }
}
