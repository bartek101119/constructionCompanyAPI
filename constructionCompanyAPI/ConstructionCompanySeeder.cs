using constructionCompanyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI
{
    public class ConstructionCompanySeeder
    {
        private readonly ConstructionCompanyDbContext dbContext;

        public ConstructionCompanySeeder(ConstructionCompanyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            if (dbContext.Database.CanConnect())
            {
                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    dbContext.SaveChanges();
                }

                if (!dbContext.ConstructionCompanies.Any())
                {
                    var constructionCompanies = GetConstructionCompanies();
                    dbContext.ConstructionCompanies.AddRange(constructionCompanies);
                    dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>() 
            { 
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                },
            };

            return roles;
        } 
        public IEnumerable<ConstructionCompany> GetConstructionCompanies()
        {
            var constructionCompanies = new List<ConstructionCompany>()
            {
                new ConstructionCompany()
                {
                    Name = "Partner Firma Budowlana",
                    LegalForm = "Spółka z Ograniczoną Odpowiedzialnością",
                    NIP = "123456789",
                    REGON = "2431341241231",
                    KRS = "13123123131",
                    ContactEmail = "heszki@wp.pl",
                    ContactNumber = "13131311",
                    StartDate = new DateTime(1998, 06, 16, 13, 45, 00),
                    Address = new Address()
                    {
                        Voivodeship = "Kujawsko-Pomorskie",
                        City = "Toruń",
                        Street = "św. Józefa",
                        PostalCode = "87-100"                      
                    },
                    CompanyOwner = new CompanyOwner()
                    {
                        FullName = "Jan Kowalski",
                        ContactEmail = "heheszki2@wp.pl",
                        ContactNumber = "232434242"
                    },
                    Employees = new List<Employee>()
                    {
                        new Employee()
                        {
                            FullName = "Andrzej Karwaszewski",
                            Specialization = "Cieśla"
                        },
                        new Employee()
                        {
                            FullName = "Marek Nowak",
                            Specialization = "Pomocnik"
                        }
                    }
                        
                },

                new ConstructionCompany()
                {
                    Name = "Partner Firma Budowlana",
                    LegalForm = "Spółka z Ograniczoną Odpowiedzialnością",
                    NIP = "1433434343",
                    REGON = "24313412443431231",
                    KRS = "1312312343433131",
                    ContactEmail = "pepega@wp.pl",
                    ContactNumber = "131311212311",
                    Address = new Address()
                    {
                        Voivodeship = "Kujawsko-Pomorskie",
                        City = "Bydgoszcz",
                        Street = "Głowackiego",
                        PostalCode = "85-614"
                    },
                    CompanyOwner = new CompanyOwner()
                    {
                        FullName = "Jan Raki",
                        ContactEmail = "heheszki1112@wp.pl",
                        ContactNumber = "1121"
                    },
                    Employees = new List<Employee>()
                    {
                        new Employee()
                        {
                            FullName = "Mateusz Karwaszewski",
                            Specialization = "Brukarz"
                        },
                        new Employee()
                        {
                            FullName = "Robert Nowak",
                            Specialization = "Pomocnik"
                        }
                    }
                },

            };
            return constructionCompanies;
        }
    }
}
