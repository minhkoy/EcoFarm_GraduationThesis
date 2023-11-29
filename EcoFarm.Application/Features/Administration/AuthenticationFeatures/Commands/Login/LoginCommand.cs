using EcoFarm.Application.Common.Results;
using EcoFarm.Application.Interfaces.Messagings_Prev;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Options;
using EcoFarm.Domain.Entities.Administration;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.Administration.AuthenticationFeatures.Commands.Login
{

    public class LoginCommand : ICommand<LoginResponse>
    {
        public string UsernameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    internal class LoginHandler : ICommandHandler<LoginCommand, LoginResponse>
    {
        private readonly IValidator<LoginCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtOption _jwtOption;
        public LoginHandler(IValidator<LoginCommand> validator,
            IUnitOfWork unitOfWork,
            IOptions<JwtOption> jwtOption)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _jwtOption = jwtOption.Value;
        }

        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid)
            {
                //Check for existance in database
                var people = _unitOfWork.Accounts
                    .GetQueryable()
                    .Where(x => x.USERNAME.Equals(request.UsernameOrEmail)
                    || x.EMAIL.Equals(request.UsernameOrEmail))
                    .FirstOrDefault();
                if (people == null)
                {
                    //throw new Exception();
                    return new BadRequestResult<LoginResponse>("Tài khoản không tồn tại", Enumerable.Empty<object>());
                }
                //...

            }
            return new BadRequestResult<LoginResponse>(string.Empty, validationResult.Errors);

        }

        private List<Claim> GenerateUserClaims(Account user)
        {

            var userRoles = _unitOfWork.RoleUsers
                .GetQueryable()
                .Where(x => x.USER_ID.Equals(user.ID))
                .ToList();
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.USERNAME),
                new Claim(ClaimTypes.Email, user.EMAIL),
                new Claim(ClaimTypes.Name, user.NAME),
                // new Claim(ClaimTypes.DATE_OF_BIRTH, user.DATE_OF_BIRTH.ToString()),
                // new Claim(ClaimTypes.MobilePhone, user.PHONE_NUMBER)
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ROLE_ID));
            }

            return claims;
        }

        //private string GenerateRefreshToken()
        //{

        //}
    }
}
