using EcoFarm.Domain.Entities;
using EcoFarm.Domain.Entities.Administration;

namespace EcoFarm.Application.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    //Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);
    Task Rollback();

    //Administration
    IGenericRepository<Account> Users { get; }
    IGenericRepository<Role> Roles { get; }
    IGenericRepository<RoleUser> RoleUsers { get; }
    IGenericRepository<SellerEnterprise> SellerEnterprises { get; }
    IGenericRepository<UserAddress> UserAddresses { get; }
    //Entities
    IGenericRepository<Order> Orders { get; }
    IGenericRepository<ServiceImage> ServiceImages { get; }
    IGenericRepository<ServicePackage> ServicePackages { get; }
    IGenericRepository<ShoppingCart> ShoppingCarts { get; }
}