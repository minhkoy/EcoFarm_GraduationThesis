using EcoFarm.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task Rollback()
        {
            //throw new NotImplementedException();
        }

        public async Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
        {
            return 0;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return 0;
        }
    }
}