using KsefCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KsefInfrastructure.EF
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<AuthorizationToken> AuthorizationToken { get; set; }
        

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Get connection string from AppSettings
            var connectionString = _configuration.GetConnectionString("KsefApiDatabase");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public override int SaveChanges()
        {
            foreach (var auditableEntity in ChangeTracker.Entries<BaseEntity>())
            {
                if (auditableEntity.State == EntityState.Added || auditableEntity.State == EntityState.Modified)
                {
                    auditableEntity.Entity.Modified = DateTime.UtcNow;

                    if(auditableEntity.State == EntityState.Added)
                        auditableEntity.Entity.Created = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}