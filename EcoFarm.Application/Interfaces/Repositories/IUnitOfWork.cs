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
    IGenericRepository<Account> Accounts { get; }
    IGenericRepository<User> Users { get; }
    IGenericRepository<Role> Roles { get; }
    IGenericRepository<RoleUser> RoleUsers { get; }
    IGenericRepository<SellerEnterprise> SellerEnterprises { get; }
    IGenericRepository<UserAddress> UserAddresses { get; }
    //Entities
    IGenericRepository<Order> Orders { get; }
    IGenericRepository<PackageMedia> PackageMedias { get; }
    IGenericRepository<FarmingPackage> FarmingPackages { get; }
    IGenericRepository<FarmingPackageActivity> FarmingPackageActivties { get; }
    IGenericRepository<ShoppingCart> ShoppingCarts { get; }
    IGenericRepository<Product> Products { get; }
    IGenericRepository<ProductMedia> ProductMedias { get; }
    IGenericRepository<UserPackageReview> PackageReviews { get; }
    IGenericRepository<Notification> Notifications { get; }
    IGenericRepository<UserRegisterPackage> UserRegisterPackages { get; }
    IGenericRepository<CartDetail> CartDetails { get; }
    IGenericRepository<OrderProduct> OrderProducts { get; }
    IGenericRepository<OrderTimeline> OrderTimelines { get; }
}