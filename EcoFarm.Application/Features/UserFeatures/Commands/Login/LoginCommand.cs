using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Options;
using EcoFarm.Domain.Entities.Administration;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.UserFeatures.Commands.Login
{

    public class LoginCommand : IRequest<LoginResponse>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    internal class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
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

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid)
            {
                //Check for existance in database
                var people = _unitOfWork.Users
                    .GetQueryable()
                    .Where(x => x.Username.Equals(request.UsernameOrEmail)
                    || x.Email.Equals(request.UsernameOrEmail))
                    .FirstOrDefault();
                if (people == null)
                {
                    //throw new Exception();
                }
                //...

            }
            return null;

        }

        private string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOption.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRoles = _unitOfWork.RoleUsers
                .GetQueryable()
                .Where(x => x.UserId.Equals(user.Id))
                .ToList();
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                // new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()),
                // new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleId));
            }

            var token = new JwtSecurityToken(issuer: _jwtOption.Issuer,
                audience: _jwtOption.Audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(50), //XXX
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //private string GenerateRefreshToken()
        //{

        //}
    }
}
