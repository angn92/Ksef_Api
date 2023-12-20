using EmployeeDiaryModel.Model;
using KsefCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KsefInfrastructure.EF
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<ClientSession> ClientSession { get; set; }
        public DbSet<Contractor> Contractor { get; set; }
        public DbSet<Address> Address { get; set; }

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
    }
}