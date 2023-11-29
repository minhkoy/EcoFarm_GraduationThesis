using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.FarmingPackages.CloseRegister
{
    public class CloseRegisterPackageCommand : ICommand<bool>
    {
        public string Id { get; set; }
        public CloseRegisterPackageCommand() { }
        public CloseRegisterPackageCommand(string id)
        {
            Id = id;
        }
    }

    internal class CloseRegisterPackageHandler : ICommandHandler<CloseRegisterPackageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CloseRegisterPackageHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(CloseRegisterPackageCommand request, CancellationToken cancellationToken)
        {
            //XXX: Validation not implemented. Just happy case.
            var package = await _unitOfWork.FarmingPackages.FindAsync(request.Id);
            if (package == null)
                return Result<bool>.NotFound("Không tìm thấy gói farming");
            package.CLOSE_REGISTER_TIME = DateTime.Now;
            _unitOfWork.FarmingPackages.Update(package);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage("Kết thúc đăng ký gói farming thành công");
        }
    }
}
