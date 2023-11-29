using EcoFarm.Application.Interfaces.Messagings_Prev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.Administration.AccountManagerFeatures.Commands.LockAccount
{
    public class LockAccountCommand : ICommand<bool>
    {
        public string AccountId { get; set; }
    }
}
