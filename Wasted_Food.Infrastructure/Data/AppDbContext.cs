using System.Reflection;
using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wasted_Food.Data.Entities;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Data.Enum;

namespace Wasted_Food.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<Users, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly IEncryptionProvider _encryptionProvider;
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

            _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");

        }

        public DbSet<Users> users { get; set; }
        public DbSet<Donations> donations { get; set; }
        public DbSet<FoodRequest> foodRequests { get; set; }
        public DbSet<UserRefreshToken> userRefreshToken { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Donations>()
             .HasMany(d => d.FoodRequests)
             .WithOne(f => f.Donation)
             .HasForeignKey(f => f.DontId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Donations>()
        .HasQueryFilter(d => !d.IsExpire && d.EndExpiryDate > DateTime.UtcNow);

            modelBuilder.Entity<FoodRequest>().Property(f => f.Status)
                 .HasConversion(
                     v => v.ToString(),
                     v => (RequestStatus)Enum.Parse(typeof(RequestStatus), v));

            modelBuilder.Entity<Users>().Property(f => f.TypeOfOrgn)
               .HasConversion(
                   v => v.ToString(),
                   v => (TypeOfOrganization)Enum.Parse(typeof(TypeOfOrganization), v));


            base.OnModelCreating(modelBuilder);
            modelBuilder.UseEncryption(_encryptionProvider);
        }
    }
}
