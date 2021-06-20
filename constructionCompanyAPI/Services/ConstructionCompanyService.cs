using AutoMapper;
using constructionCompanyAPI.Authorization;
using constructionCompanyAPI.Entities;
using constructionCompanyAPI.Exceptions;
using constructionCompanyAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Services
{
    public interface IConstructionCompanyService
    {
        int Create(CreateConstructionCompanyDto dto);
        PagedResult<ConstructionCompanyDto> GetAll(ConstructionCompanyQuery query);
        ConstructionCompanyDto GetById(int id);
        void Delete(int id);
        void Put(UpdateConstructionCompanyDto dto, int id);
    }

    public class ConstructionCompanyService : IConstructionCompanyService
    {
        private readonly ConstructionCompanyDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ConstructionCompanyService> logger;
        private readonly IAuthorizationService authorizationService;
        private readonly IUserContextService userContextService;

        public ConstructionCompanyService(ConstructionCompanyDbContext dbContext, IMapper mapper, ILogger<ConstructionCompanyService> logger, 
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
            this.authorizationService = authorizationService;
            this.userContextService = userContextService;
        }
        public PagedResult<ConstructionCompanyDto> GetAll(ConstructionCompanyQuery query)
        {
            var baseQuery = dbContext
                .ConstructionCompanies
                .Include(c => c.Address)
                .Include(c => c.CompanyOwner)
                .Include(c => c.Employees)
                .Where(p => query.SearchPhrase == null || p.Name.ToLower().Contains(query.SearchPhrase) || p.NIP.ToLower().Contains(query.SearchPhrase)); // zwraca wszystkie zasoby lub po właściwościach


            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<ConstructionCompany, object>>>
                {
                    {nameof(ConstructionCompany.Name), r => r.Name },
                    {nameof(ConstructionCompany.LegalForm), r => r.LegalForm },
                    {nameof(ConstructionCompany.NIP), r => r.NIP}
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var constructionCompanies = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1)) // pomijam określoną liczbę elementów
                .Take(query.PageSize) // następnie pobieram tyle elementów ile potrzebuję (przydatne do paginacji danych) 
                .ToList();

            var totalItemsCount = baseQuery.Count();

            var constructionCompaniesDtos = mapper.Map<List<ConstructionCompanyDto>>(constructionCompanies);

            var result = new PagedResult<ConstructionCompanyDto>(constructionCompaniesDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public ConstructionCompanyDto GetById(int id)
        {
            var constructionCompany = dbContext
                .ConstructionCompanies
                .Include(c => c.Address)
                .Include(c => c.CompanyOwner)
                .Include(c => c.Employees)
                .FirstOrDefault(c => c.Id == id);

            if (constructionCompany is null)
                throw new NotFoundException("Construction Company not found");

            var constructionCompanyDto = mapper.Map<ConstructionCompanyDto>(constructionCompany);

            return constructionCompanyDto;
        }

        public int Create(CreateConstructionCompanyDto dto)
        {
            var constructionCompany = mapper.Map<ConstructionCompany>(dto);
            constructionCompany.CreatedById = userContextService.GetUserId;
            dbContext.ConstructionCompanies.Add(constructionCompany);
            dbContext.SaveChanges();

            return constructionCompany.Id;
        }

        public void Delete(int id)
        {
            logger.LogError($"Construction Company with id: {id} DELETE action invoked");

            var constructionCompany = dbContext
                .ConstructionCompanies
                .FirstOrDefault(c => c.Id == id);

            if (constructionCompany is null)
                throw new NotFoundException("Construction Company not found");

            var authorizationResult = authorizationService.AuthorizeAsync(userContextService.User, constructionCompany, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }


            dbContext.ConstructionCompanies.Remove(constructionCompany);
            dbContext.SaveChanges();

        }

        public void Put(UpdateConstructionCompanyDto dto, int id)
        {
            

            var constructionCompany = dbContext
                .ConstructionCompanies
                .FirstOrDefault(c => c.Id == id);

            if (constructionCompany is null)
                throw new NotFoundException("Construction Company not found");

            var authorizationResult = authorizationService.AuthorizeAsync(userContextService.User, constructionCompany, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            constructionCompany.Name = dto.Name;
            constructionCompany.LegalForm = dto.LegalForm;
            constructionCompany.KRS = dto.KRS;
            constructionCompany.NIP = dto.NIP;
            constructionCompany.REGON = dto.REGON;
            constructionCompany.StartDate = dto.StartDate;


            dbContext.SaveChanges();

        }
    }
}
