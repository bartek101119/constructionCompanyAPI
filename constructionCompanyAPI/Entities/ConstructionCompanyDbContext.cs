using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Entities
{
    public class ConstructionCompanyDbContext : DbContext
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ConstructionCompanyDb;Trusted_Connection=True;";
        public DbSet<ConstructionCompany> ConstructionCompanies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CompanyOwner> CompanyOwners { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConstructionCompany>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(40);

            modelBuilder.Entity<CompanyOwner>()
                .Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(40);



        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
