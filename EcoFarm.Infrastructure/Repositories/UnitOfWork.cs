using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities;
using EcoFarm.Domain.Entities.Administration;
using EcoFarm.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcoContext _ecoContext;

        public UnitOfWork(EcoContext ecoContext)
        {
            _ecoContext = ecoContext;
        }

        public IGenericRepository<User> Users { get; private set; }

        public IGenericRepository<Role> Roles { get; private set; }

        public IGenericRepository<RoleUser> RoleUsers { get; private set; }

        public IGenericRepository<SellerEnterprise> SellerEnterprises { get; private set; }

        public IGenericRepository<UserAddress> UserAddresses { get; private set; }

        public IGenericRepository<Order> Orders { get; private set; }

        public IGenericRepository<ServiceImage> ServiceImages { get; private set; }

        public IGenericRepository<ServicePackage> ServicePackages { get; private set; }

        public IGenericRepository<ShoppingCart> ShoppingCarts { get; private set; }

        public void Dispose()
        {
            _ecoContext.Dispose();
        }

        public Task Rollback()
        {
            _ecoContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }

        //public async Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
        //{
        //    return _ecoContext.ChangeTracker.Clear();
        //}

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _ecoContext.SaveChangesAsync();
        }
    }
}