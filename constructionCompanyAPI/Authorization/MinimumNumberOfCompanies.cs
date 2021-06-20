using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Authorization
{
    public class MinimumNumberOfCompanies : IAuthorizationRequirement
    {
        public int MinimumCompanies { get; }

        public MinimumNumberOfCompanies(int minimumCompanies)
        {
            MinimumCompanies = minimumCompanies;
        }
    }
}
