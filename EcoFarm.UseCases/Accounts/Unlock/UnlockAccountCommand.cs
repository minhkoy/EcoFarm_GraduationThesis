using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities.Administration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.Accounts.Unlock
{
    public class UnlockAccountCommand : ICommand<bool>
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }

    internal class UnlockAccountHandler : ICommandHandler<UnlockAccountCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnlockAccountHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(UnlockAccountCommand request, CancellationToken cancellationToken)
        {
            Account account = null;
            if (!string.IsNullOrEmpty(request.Id))
            {
                account = await _unitOfWork.Accounts.FindAsync(request.Id);
            }
            else
            {
                account = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.USERNAME.Equals(request.Username));
            }
            if (account is null)
            {
                return Result.NotFound("Không tìm thấy thông tin tài khoản. Vui lòng kiểm tra lại");
            }
            account.IS_ACTIVE = true;
            account.LOCKED_REASON = string.Empty;
            _unitOfWork.Accounts.Update(account);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
