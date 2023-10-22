using EcoFarm.Application.Extensions;
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
        public CreateServiceValidator()
        {
            RuleFor(x => x.ServiceCode).NotEmpty()
                .WithName(Constants.ValidationErrorNames.MissingRequiredInformation)
                .WithMessage(
                    HelperExtensions.StringAfterFormatting(Constants.ValidationErrorMsgs.MissingRequiredInformation,
                        "Service code"));
            RuleFor(x => x.ServiceName).NotEmpty()
                .WithMessage(Constants.ValidationErrorMsgs.MissingRequiredInformationDetail);
        }
    }
}