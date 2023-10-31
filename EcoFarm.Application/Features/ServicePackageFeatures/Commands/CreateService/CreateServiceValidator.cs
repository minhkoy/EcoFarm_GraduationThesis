using EcoFarm.Application.Extensions;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Domain.Common.Values.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.ServicePackageFeatures.Commands.CreateService
{
    public class CreateServiceValidator : AbstractValidator<CreateServiceCommand>
    {
        private readonly ILocalizeService _localizeService;
        public CreateServiceValidator(ILocalizeService localizeService)
        {
            _localizeService = localizeService;
            //RuleFor(x => x.ServiceCode).NotEmpty()
            //    .WithName(EFX.ValidationErrorNames.MissingRequiredInformation)
            //    .WithMessage(
            //        HelperExtensions.StringAfterFormatting(EFX.ValidationErrorMsgs.MissingRequiredInformation,
            //            "Service code"));
            RuleFor(x => x.ServiceName).NotEmpty()
                .WithMessage(EFX.ValidationErrorMsgs.MissingRequiredInformationDetail);
            RuleFor(x => x.EnterpriseId).NotEmpty()
                .WithMessage("Cần có thông tin doanh nghiệp");
        }
    }
}