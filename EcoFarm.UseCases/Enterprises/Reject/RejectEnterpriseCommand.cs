using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.Enterprises.Reject
{
    public class RejectEnterpriseCommand : ICommand<bool>
    {
        public string EnterpriseId { get; set; }
        public string RejectReason { get; set; }
    }

    internal class RejectEnterpriseHander : ICommandHandler<RejectEnterpriseCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RejectEnterpriseHander(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(RejectEnterpriseCommand request, CancellationToken cancellationToken)
        {
            var enterprise = await _unitOfWork.SellerEnterprises
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.ID.Equals(request.EnterpriseId));
            if (enterprise is null)
            {
                return Result<bool>.NotFound("Không tìm thấy thông tin doanh nghiệp");
            }
            if (!enterprise.IS_ACTIVE)
            {
                var account = await _unitOfWork.Accounts.FindAsync(enterprise.ACCOUNT_ID);
                return Result.Error($"Doanh nghiệp bị khóa vì ${account.LOCKED_REASON}. Vui lòng thử lại sau.");
            }
            enterprise.IS_APPROVED = false;
            enterprise.APPROVED_OR_REJECTED_TIME = DateTime.Now.ToVnDateTime();
            enterprise.REJECT_REASON = request.RejectReason;

            _unitOfWork.SellerEnterprises.Update(enterprise);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage($"Doanh nghiệp {enterprise.NAME} đã bị từ chối.");
        }
    }
}
