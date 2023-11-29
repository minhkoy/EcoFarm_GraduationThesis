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

namespace EcoFarm.UseCases.Accounts.Lock
{
    public class LockAccountCommand : ICommand<bool>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Reason { get; set; }
    }

    internal class LockAccountHandler : ICommandHandler<LockAccountCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly
        public LockAccountHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(LockAccountCommand request, CancellationToken cancellationToken)
        {
            Account account = null;
            if (!string.IsNullOrEmpty(request.Id))
            {
                account = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.ID.Equals(request.Id));
            }
            else if (!string.IsNullOrEmpty(request.Username))
            {
                account = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.USERNAME.Equals(request.Username));
            }
            if (account is null)
            {
                return Result<bool>.NotFound("Không tìm thấy tài khoản");
            }

            account.IS_ACTIVE = false;
            account.LOCKED_REASON = request.Reason;
            _unitOfWork.Accounts.Update(account);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
