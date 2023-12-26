using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //seed Roles(User, Admin, SuperAdmin)
            var adminRoleId = "0b619192-9936-4431-a0f5-098d20bc81d5";
            var superAdminRoleId = "9efc8aef-dabd-489e-bbe9-75b50cd8c0a7";
            var userRoleId = "09fbd99d-c9dc-4735-86bc-4eb7e0e84e54";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name ="SuperAdmin",
                    NormalizedName ="SuperAdmin",
                    Id= superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="User",
                    Id= userRoleId,
                    ConcurrencyStamp = userRoleId

                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            //seed SuperAdminUser
            var superAdminId = "cda35300-0be0-4987-b5d4-28d715bdf237";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id = superAdminId,
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Superadmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);
            //Add all roles to superAdminUser
            
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>()
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId,
                },
                new IdentityUserRole<string>()
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId,
                },
                new IdentityUserRole<string>()
                {
                    RoleId = userRoleId,
                    UserId = superAdminId,
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
