using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities.Administration;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public LoginHandler(IValidator<LoginCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
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
            return string.Empty;
        }
    }
}
