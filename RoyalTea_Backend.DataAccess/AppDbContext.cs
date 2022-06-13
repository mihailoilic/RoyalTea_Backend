using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Domain;
using System;

namespace RoyalTea_Backend.DataAccess
{
    public class AppDbContext : DbContext
    {
        public IAppUser AppUser { get; }

        public AppDbContext(DbContextOptions options, IAppUser appUser)
            : base(options)
        {

            this.AppUser = appUser;
        }

        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIHAILOPC\\SQLEXPRESS;Initial Catalog=RoyalTea;Integrated Security=True").UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<CategorySpecification>().HasKey(x => new { x.SpecificationId, x.CategoryId });
            modelBuilder.Entity<UseCase>().HasKey(x => new { x.UseCaseId, x.UserId });
            modelBuilder.Entity<ProductSpecificationValue>().HasKey(x => new { x.ProductId, x.SpecificationValueId });

            base.OnModelCreating(modelBuilder); 
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.IsActive = true;
                            e.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.UtcNow;
                            e.UpdatedBy = AppUser?.Username;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<SpecificationValue> SpecificationValues { get; set; }
        public DbSet<CategorySpecification> CategorySpecifications { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSpecificationValue> ProductSpecificationValues { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Price> Prices { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UseCase> UseCases { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<CartItem> CartItems{ get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
