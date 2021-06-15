using constructionCompanyAPI.Entities;
using constructionCompanyAPI.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<User> passwordHasher;

        public AccountService(ConstructionCompanyDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
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

            var hashedPassword = passwordHasher.HashPassword(user, dto.Password);
            user.PasswordHash = hashedPassword;
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }
    }
}
