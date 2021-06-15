using constructionCompanyAPI.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        

        public RegisterUserDtoValidator(ConstructionCompanyDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(p => p.Password);

            RuleFor(x => x.Email).Custom((value, context) => 
            {
                var emailInUse = dbContext.Users.Any(x => x.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "That email is taken");
                }
            
            });
           
        }
    }
}
