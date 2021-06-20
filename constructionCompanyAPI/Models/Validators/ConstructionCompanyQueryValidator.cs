using constructionCompanyAPI.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Models.Validators
{
    public class ConstructionCompanyQueryValidator : AbstractValidator<ConstructionCompanyQuery>
    {

        private int[] allowedPageSizes = new[] { 5, 10, 15 };
        private string[] allowedSortByColumnsNames = new[] { nameof(ConstructionCompany.Name), nameof(ConstructionCompany.LegalForm), nameof(ConstructionCompany.NIP) };
        public ConstructionCompanyQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) => 
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }        
            
            });

            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnsNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnsNames)}]");
        }

    }
}
