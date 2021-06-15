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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(n => n.Name)
                .IsRequired();

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
