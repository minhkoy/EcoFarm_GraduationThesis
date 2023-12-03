using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.Accounts.Logout
{
    public class LogoutAccountCommand : ICommand<bool>
    {

    }

    public class LogoutAccountCommandHandler : ICommandHandler<LogoutAccountCommand, bool>
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        public LogoutAccountCommandHandler(IAuthService authService, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(LogoutAccountCommand request, CancellationToken cancellationToken)
        {
            var username = _authService.GetUsername();
            if (string.IsNullOrEmpty(username))
            {
                return Result<bool>.Unauthorized();
            }
            var account = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.USERNAME.Equals(username));
            if (account is null || account.IS_DELETE)
            {
                return Result<bool>.Error("Thông tin tài khoản không chính xác");
            }
            if (account.LAST_LOGGED_IN < account.LAST_LOGGED_OUT)
            {
                return Result<bool>.Error($"Đã đăng xuất tài khoản {username}");
            }

            account.LAST_LOGGED_OUT = DateTime.Now.ToVnDateTime();
            _unitOfWork.Accounts.Update(account);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
