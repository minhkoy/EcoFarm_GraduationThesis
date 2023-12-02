using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities;
using EcoFarm.Domain.Entities.Administration;
using EcoFarm.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EcoContext _ecoContext;
        public UnitOfWork(EcoContext ecoContext, IAuthService authService)
        {
            _ecoContext = ecoContext;
            Accounts = new GenericRepository<Account>(_ecoContext, authService);
            Users = new GenericRepository<User>(_ecoContext, authService);
            Roles = new GenericRepository<Role>(_ecoContext, authService);
            RoleUsers = new GenericRepository<RoleUser>(_ecoContext, authService);
            SellerEnterprises = new GenericRepository<SellerEnterprise>(_ecoContext, authService);
            UserAddresses = new GenericRepository<UserAddress>(_ecoContext, authService);
            Orders = new GenericRepository<Order>(_ecoContext, authService);
            OrderProducts = new GenericRepository<OrderProduct>(_ecoContext, authService);
            OrderTimelines = new GenericRepository<OrderTimeline>(_ecoContext, authService);
            PackageMedias = new GenericRepository<PackageMedia>(_ecoContext, authService);
            FarmingPackages = new GenericRepository<FarmingPackage>(_ecoContext, authService);
            FarmingPackageActivties = new GenericRepository<FarmingPackageActivity>(_ecoContext, authService);
            ShoppingCarts = new GenericRepository<ShoppingCart>(_ecoContext, authService);
            Products = new GenericRepository<Product>(_ecoContext, authService);
            ProductMedias = new GenericRepository<ProductMedia>(_ecoContext, authService);
            PackageReviews = new GenericRepository<UserPackageReview>(_ecoContext, authService);
            Notifications = new GenericRepository<Notification>(_ecoContext, authService);
            UserRegisterPackages = new GenericRepository<UserRegisterPackage>(_ecoContext, authService);
            CartDetails = new GenericRepository<CartDetail>(_ecoContext, authService);
            //Users = new GenericRepository<Account>(_ecoContext);
        }

        public int SaveChanges()
        {
            try
            {
                return _ecoContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //return -1;
                //XXXX
                foreach (var entry in ex.Entries)
                {
                    var proposedValues = entry.CurrentValues;
                    var databaseValues = entry.GetDatabaseValues();

                    foreach(var property in proposedValues.Properties)
                    {
                        //var proposedValue = proposedValues[property];
                        var databaseValue = databaseValues[property];
                        proposedValues[property] = databaseValue;

                    }
                    entry.OriginalValues.SetValues(databaseValues);
                }
                return -1;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _ecoContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //return -1;
                //XXXX
                foreach (var entry in ex.Entries)
                {
                    var proposedValues = entry.CurrentValues;
                    var databaseValues = entry.GetDatabaseValues();

                    foreach (var property in proposedValues.Properties)
                    {
                        //var proposedValue = proposedValues[property];
                        var databaseValue = databaseValues[property];
                        proposedValues[property] = databaseValue;

                    }
                    entry.OriginalValues.SetValues(databaseValues);
                }
                return -1;
            }
        }

        public IGenericRepository<Account> Accounts { get; private set; }
        public IGenericRepository<User> Users { get; private set; }

        public IGenericRepository<Role> Roles { get; private set; }

        public IGenericRepository<RoleUser> RoleUsers { get; private set; }

        public IGenericRepository<SellerEnterprise> SellerEnterprises { get; private set; }

        public IGenericRepository<UserAddress> UserAddresses { get; private set; }

        public IGenericRepository<Order> Orders { get; private set; }

        public IGenericRepository<PackageMedia> PackageMedias { get; private set; }

        public IGenericRepository<FarmingPackage> FarmingPackages { get; private set; }

        public IGenericRepository<FarmingPackageActivity> FarmingPackageActivties { get; private set; }

        public IGenericRepository<ShoppingCart> ShoppingCarts { get; private set; }
        
        public IGenericRepository<Product> Products { get; private set; }

        public IGenericRepository<ProductMedia> ProductMedias { get; private set; }

        public IGenericRepository<UserPackageReview> PackageReviews { get; private set; }

        public IGenericRepository<Notification> Notifications { get; private set; }

        public IGenericRepository<UserRegisterPackage> UserRegisterPackages { get; private set; }

        public IGenericRepository<CartDetail> CartDetails { get; private set; }

        public IGenericRepository<OrderProduct> OrderProducts { get; private set; }

        public IGenericRepository<OrderTimeline> OrderTimelines { get; private set; }

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