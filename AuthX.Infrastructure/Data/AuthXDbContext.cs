using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Infrastructure.Data
{
    public class AuthXDbContext : IdentityDbContext
    {
        public AuthXDbContext(DbContextOptions<AuthXDbContext> options) : base(options)
        {
        }

        protected AuthXDbContext()
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var UserRoleId = "2f11e208-d65f-4128-9752-d23683c6a5ad";
            var AdminRoleId = "5f104183-fab5-4db5-96fc-e3db8918f758";


            var Role = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=UserRoleId,
                    ConcurrencyStamp=UserRoleId,
                    Name="User",
                    NormalizedName="User".ToUpper()
                },
                 new IdentityRole
                {
                    Id=AdminRoleId,
                    ConcurrencyStamp=AdminRoleId,
                    Name="Admin",
                    NormalizedName="Admin".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(Role);

        }
    }
}
