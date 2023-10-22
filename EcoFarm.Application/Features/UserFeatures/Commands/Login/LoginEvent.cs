using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.UserFeatures.Commands.Login
{
    public class LoginEvent : BaseEvent
    {
        public User UserLoggedIn { get; set; }

        public LoginEvent(User userLoggedIn)
        {
            UserLoggedIn = userLoggedIn;
        }
    }
}
