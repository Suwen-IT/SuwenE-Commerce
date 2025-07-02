using Domain.Entities;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Basket > Baskets { get; set; } 
        public DbSet<BasketItem> BasketItems { get; set; }

    }
}
