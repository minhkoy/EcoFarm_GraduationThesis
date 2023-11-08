using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Localization;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.Administration.AuthenticationFeatures.Commands.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        private readonly ILocalizeService _localizeService;
        public LoginValidator(ILocalizeService localizeService)
        {
            _localizeService = localizeService;
            RuleFor(x => x.UsernameOrEmail).NotEmpty()
                .WithName("USERNAME")
                .WithMessage(_localizeService.GetMessage(LocalizationEnum.MissingRequiredFields));
            RuleFor(x => x.Password).NotEmpty()
                .WithName("Password")
                .WithMessage(_localizeService.GetMessage(LocalizationEnum.MissingRequiredFields));
        }
    }
}
