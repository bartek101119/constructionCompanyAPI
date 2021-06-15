using constructionCompanyAPI.Entities;
using constructionCompanyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Services
{
    public interface IAccountService
    {
        void Register(RegisterUserDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly ConstructionCompanyDbContext dbContext;

        public AccountService(ConstructionCompanyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Register(RegisterUserDto dto)
        {
            var user = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }
    }
}
