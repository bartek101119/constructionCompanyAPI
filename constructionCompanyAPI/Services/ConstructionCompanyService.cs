using AutoMapper;
using constructionCompanyAPI.Entities;
using constructionCompanyAPI.Exceptions;
using constructionCompanyAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Services
{
    public interface IConstructionCompanyService
    {
        int Create(CreateConstructionCompanyDto dto);
        IEnumerable<ConstructionCompanyDto> GetAll();
        ConstructionCompanyDto GetById(int id);
        void Delete(int id);
        void Put(UpdateConstructionCompanyDto dto, int id);
    }

    public class ConstructionCompanyService : IConstructionCompanyService
    {
        private readonly ConstructionCompanyDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ConstructionCompanyService> logger;

        public ConstructionCompanyService(ConstructionCompanyDbContext dbContext, IMapper mapper, ILogger<ConstructionCompanyService> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }
        public IEnumerable<ConstructionCompanyDto> GetAll()
        {
            var constructionCompanies = dbContext
                .ConstructionCompanies
                .Include(c => c.Address)
                .Include(c => c.CompanyOwner)
                .Include(c => c.Employees)
                .ToList();

            var constructionCompaniesDtos = mapper.Map<List<ConstructionCompanyDto>>(constructionCompanies);

            return constructionCompaniesDtos;
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
