using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Repositories;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.Common.BackgroundJobs.BatchJobs
{
    public class AutoCloseRegisterPackageJob
    {
        private readonly IUnitOfWork _unitOfWork;
        public AutoCloseRegisterPackageJob(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Run(string packageId, DateTime closeRegisterTime)
        {
            var package = await _unitOfWork.FarmingPackages.FindAsync(packageId);
            if (package is null || package.CLOSE_REGISTER_TIME.HasValue || !package.IS_AUTO_CLOSE_REGISTER)
            {
                return;
            }
            package.CLOSE_REGISTER_TIME = closeRegisterTime.ToVnDateTime();
            _unitOfWork.FarmingPackages.Update(package);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
