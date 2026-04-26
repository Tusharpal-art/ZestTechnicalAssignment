using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZestTechnicalAssignment.Domain.Entities;

namespace ZestTechnicalAssignment.DataAccess.ApplicationContext
{
    public class ApplicationDBContext(DbContextOptions dbContext):IdentityDbContext <User,IdentityRole<Guid>,Guid>(dbContext)
    {
       public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder) ;

            modelBuilder.Entity<Student>(entity => {
                entity.HasOne(item => item.CreatedBy)
                .WithMany(entity => entity.Students)
                .HasForeignKey(item => item.CreatedById)
                .OnDelete(DeleteBehavior.SetNull);
                });

            var UserRoleId = Guid.Parse("47C6B15C-1D6E-4D4D-ACD5-A6CF135426F2");
            var AdminRoleId = Guid.Parse("782CAA12-83F2-42E6-8C0E-7A4CD67277C5");

            List<IdentityRole<Guid>> Roles = [
                new IdentityRole<Guid>(){
                    Id = AdminRoleId,
                    ConcurrencyStamp= AdminRoleId.ToString(),
                    Name="Admin",
                    NormalizedName="Admin".ToUpper()
                },
                new IdentityRole<Guid>(){
                    Id=UserRoleId,
                    ConcurrencyStamp = UserRoleId.ToString(),
                    Name="User",
                    NormalizedName="User".ToUpper()
                }
            ];

            modelBuilder.Entity<IdentityRole<Guid>>().HasData(Roles);

            var AdminId = Guid.Parse("7012418B-8130-4533-B214-E0E449CD43AA");
            var hasher = new PasswordHasher<User>();
            var adminUser = new User
            {
                Id = AdminId,
                Name = "Tushar Pal",
                UserName = "tushar@gmail.com",
                NormalizedUserName = "tushar@gmail.com".ToUpper(),
                Email = "tushar@gmail.com",
                NormalizedEmail = "tushar@gmail.com".ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = AdminId.ToString("D"),
                ConcurrencyStamp = AdminId.ToString("D"),
                PasswordHash = "AQAAAAIAAYagAAAAEMHOU0ljZF4uKsvL+x+hTbWutGvjl5plOpHiAK6Db0tHwMGdx2zENOfQckn1+VM2gw=="
            };

            modelBuilder.Entity<User>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                UserId = AdminId,
                RoleId = AdminRoleId
            });
        }

    }

   }

