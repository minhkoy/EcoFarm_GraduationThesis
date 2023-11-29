using EcoFarm.Application.Common.Results;
using EcoFarm.Application.Interfaces.Messagings_Prev;
using EcoFarm.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.Administration.AuthenticationFeatures.Commands.ForgetPassword
{
    public class ForgetPasswordCommand : ICommand<bool>
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class ForgetPasswordHandler : ICommandHandler<ForgetPasswordCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ForgetPasswordHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            //XXXX
            throw new NotImplementedException();
        }
    }
}
