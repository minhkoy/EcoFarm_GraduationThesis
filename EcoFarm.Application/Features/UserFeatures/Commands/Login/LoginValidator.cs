using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Localization;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.UserFeatures.Commands.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        private readonly ILocalizeService _localizeService;
        public LoginValidator(ILocalizeService localizeService)
        {
            _localizeService = localizeService;
            RuleFor(x => x.UsernameOrEmail).NotEmpty()
                .WithMessage(_localizeService.GetMessage(LocalizationEnum.UsernameOrPasswordEmpty));
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage(_localizeService.GetMessage(LocalizationEnum.UsernameOrPasswordEmpty));
        }
    }
}
