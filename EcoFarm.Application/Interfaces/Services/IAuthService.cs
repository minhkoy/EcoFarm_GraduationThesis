using EcoFarm.Domain.Entities.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Interfaces.Services
{
    public interface IAuthService
    {
        //Get user info from JWT Token
        Task<User> GetUserInfoByToken();
    }
}
