using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.Accounts.GetVerificationCode
{
    public class GetVerificationCodeCommand : ICommand<bool>
    {
        public string VerifyReason { get; set; }
    }

    internal class Handler : ICommandHandler<GetVerificationCodeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public Handler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<bool>> Handle(GetVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.Accounts.FindAsync(_authService.GetAccountEntityId());
            if (account is null)
            {
                return Result.Error("Tài khoản không tồn tại hoặc đã bị xóa");
            }
            var verificationCode = HelperExtensions.GetRandomString(6);
            MailAddress to = new MailAddress(account.EMAIL);
            MailAddress from = new MailAddress("minhcutepikoy@gmail.com");
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Mã xác thực tài khoản";
            message.Body = $"Mã xác thực tài khoản của bạn là: {verificationCode}";
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(userName: "", password: "");
            throw new NotImplementedException();
        }

    }
}
