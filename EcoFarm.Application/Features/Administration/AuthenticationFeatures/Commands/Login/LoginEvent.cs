using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.Administration.AuthenticationFeatures.Commands.Login
{
    public class LoginEvent : BaseEvent
    {
        public Account UserLoggedIn { get; set; }

        public LoginEvent(Account userLoggedIn)
        {
            UserLoggedIn = userLoggedIn;
        }
    }
}
