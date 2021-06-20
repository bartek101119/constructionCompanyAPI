using constructionCompanyAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Authorization
{
    public class MinimumNumberOfCompaniesHandler : AuthorizationHandler<MinimumNumberOfCompanies>
    {
        private readonly ConstructionCompanyDbContext dbContext;

        public MinimumNumberOfCompaniesHandler(ConstructionCompanyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumNumberOfCompanies requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var CompaniesCount = dbContext.ConstructionCompanies.Count(c => c.CreatedById == userId);

            if (CompaniesCount >= requirement.MinimumCompanies)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
