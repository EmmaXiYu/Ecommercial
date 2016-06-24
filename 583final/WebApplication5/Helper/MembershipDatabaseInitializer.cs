using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;




using System;
using System.Globalization;
using System.Linq;
using System.Data.Entity;
using WebApplication5.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication5.Helpers;

namespace WebApplication5.Helper
{
    public class MembershipDatabaseInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {

            GetRoles().ForEach(c => context.Roles.Add(c));
            context.SaveChanges();
            ApplicationPasswordHasher hasher = new ApplicationPasswordHasher();
            var user = new ApplicationUser { UserName = "admin", Email = "admin@admin.com", PasswordHash = hasher.HashPassword("admin") };
            var role = context.Roles.Where(r => r.Name == "Admin").First();
            user.Roles.Add(new IdentityUserRole { RoleId = role.Id, UserId = user.Id });
            context.Users.Add(user);
            context.SaveChanges();
            base.Seed(context);
        }

        private static List<ApplicationRole> GetRoles()
        {
            var roles = new List<ApplicationRole> {
               new ApplicationRole {Name="Admin", Description="Admin"},
                new ApplicationRole {Name="RegularMember", Description="RegularMember"},
                new ApplicationRole {Name="SilverMember", Description="SilverMember"},
                  new ApplicationRole {Name="GoldenMember", Description="GoldenMember"}

            };

            return roles;
        }
    }
}