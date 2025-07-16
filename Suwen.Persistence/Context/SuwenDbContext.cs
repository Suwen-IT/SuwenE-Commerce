using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Base;

namespace Persistence.Context
{
    public class SuwenDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public SuwenDbContext(DbContextOptions<SuwenDbContext> options):base(options) 
        { 
      
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Notification>Notifications { get; set; }
        public DbSet<Basket > Baskets { get; set; } 
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<AppRole>().ToTable("Roles");

            builder.Entity<Address>()
                .HasOne(a => a.AppUser)
                .WithMany(a => a.Addresses)
                .HasForeignKey(a => a.AppUserId);
            
            builder.Entity<Basket>()
                .HasOne(a => a.AppUser)
                .WithMany(a => a.Baskets)
                .HasForeignKey(a => a.AppUserId);
            
            builder.Entity<BasketItem>()
                .HasOne(bi => bi.Basket)
                .WithMany(b => b.BasketItems)
                .HasForeignKey(bi => bi.BasketId);
            
            builder.Entity<BasketItem>()
                .HasOne(bi => bi.Product)
                .WithMany(b => b.BasketItems)
                .HasForeignKey(bi => bi.ProductId);
            
            builder.Entity<BasketItem>()
                .HasOne(bi=>bi.ProductAttributeValue)
                .WithMany()
                .HasForeignKey(bi=>bi.ProductAttributeValueId)
                .IsRequired(false);
            
            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
            
            builder.Entity<Notification>()
                .HasOne(o => o.AppUser)
                .WithMany(u => u.Notifications)
                .HasForeignKey(o => o.AppUserId);
            
            builder.Entity<Order>()
                .HasOne(o => o.AppUser)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.AppUserId);

            builder.Entity<Order>()
                .HasOne(o => o.ShippingAddress)
                .WithMany(a => a.ShippingOrders)
                .HasForeignKey(o => o.ShippingAddressId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.Entity<Order>()
                .HasOne(o => o.BillingAddress)
                .WithMany(a => a.BillingOrders)
                .HasForeignKey(o => o.BillingAddressId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);
            
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.ProductAttributeValue)
                .WithMany()
                .HasForeignKey(oi => oi.ProductAttributeValueId)
                .IsRequired(false);
            
            builder.Entity<ProductAttributeValue>()
                .HasOne(pav => pav.ProductAttribute)
                .WithMany(pav => pav.ProductAttributeValues)
                .HasForeignKey(pav => pav.ProductAttributeId);
            
            builder.Entity<ProductAttributeValue>()
                .HasOne(pav => pav.Product)
                .WithMany(pav => pav.ProductAttributeValues)
                .HasForeignKey(pav => pav.ProductId);
            
            builder.Entity<Review>()
                .HasOne(r=>r.Product)
                .WithMany(r=>r.Reviews)
                .HasForeignKey(r=>r.ProductId);
            
            builder.Entity<Review>()
                .HasOne(r=>r.AppUser)
                .WithMany(r=>r.Reviews)
                .HasForeignKey(r=>r.AppUserId);
            
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
                
            }
            return base.SaveChanges();
        }

    }
}
