using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.FarmingPackages.Start
{
    public class StartFarmingPackageValidator : AbstractValidator<StartFarmingPackageCommand>
    {
        public StartFarmingPackageValidator()
        {
            RuleFor(x => x.FarmingPackageId)
                .NotEmpty()
                .WithMessage("Vui lòng nhập thông tin gói farming");
        }
    }
}
