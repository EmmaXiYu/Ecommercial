using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WebApplication5.Helper;

namespace WebApplication5.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("WebApplicationContext")
        {
            Database.SetInitializer<ApplicationContext>(new MembershipDatabaseInitializer());
     
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCart { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

    }
}