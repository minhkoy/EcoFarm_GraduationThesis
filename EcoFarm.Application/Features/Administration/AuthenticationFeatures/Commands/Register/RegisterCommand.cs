using EcoFarm.Application.Interfaces.Messagings_Prev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Application.Features.Administration.AuthenticationFeatures.Commands.Register
{
    public class RegisterCommand : ICommand<bool>
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AccountType Role { get; set; }

    }
}
