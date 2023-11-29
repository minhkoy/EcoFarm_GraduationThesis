using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities.Administration;
using EcoFarm.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.Infrastructure.Repositories.Extended
{
    public class UserRepository : GenericRepository<Account>
    {
        public UserRepository(EcoContext ecoContext, IAuthService authService) : base(ecoContext, authService)
        {
        }

        public async Task<Account> GetUserByUsername(string username)
        {
            return await _ecoContext.Users
                .Where(x => x.USERNAME.Equals(username))
                .FirstOrDefaultAsync();
        }

        public async Task<Account> GetUserByEmail(string email)
        {
            return await _ecoContext.Users
                .Where(x => x.EMAIL.Equals(email))
                .FirstOrDefaultAsync();
        }


    }
}
