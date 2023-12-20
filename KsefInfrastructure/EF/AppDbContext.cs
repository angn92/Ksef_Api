using EmployeeDiaryModel.Model;
using KsefCore.Model;
using Microsoft.EntityFrameworkCore;

namespace KsefInfrastructure.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<ClientSession> ClientSession { get; set; }
        public DbSet<Contractor> Contractor { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDB;Trusted_Connection=True;");
        }
    }
}