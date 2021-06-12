using AutoMapper;
using constructionCompanyAPI.Entities;
using constructionCompanyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Controllers
{
    [Route("/api/constructionCompany")]
    public class ConstructionCompanyController : ControllerBase
    {
        private readonly ConstructionCompanyDbContext dbContext;
        private readonly IMapper mapper;

        public ConstructionCompanyController(ConstructionCompanyDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        public ActionResult CreateConstructionCompany([FromBody] CreateConstructionCompanyDto dto)
        {
            var constructionCompany = mapper.Map<ConstructionCompany>(dto);
            dbContext.ConstructionCompanies.Add(constructionCompany);
            dbContext.SaveChanges();

            return Created($"api/constructionCompany/{constructionCompany.Id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ConstructionCompanyDto>> GetAll()
        {
            var constructionCompanies = dbContext
                .ConstructionCompanies
                .Include(c => c.Address)
                .Include(c => c.CompanyOwner)
                .Include(c => c.Employees)
                .ToList();

            var constructionCompaniesDtos = mapper.Map<List<ConstructionCompanyDto>>(constructionCompanies);

            return Ok(constructionCompaniesDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ConstructionCompanyDto> Get([FromRoute]int id)
        {
            var constructionCompany = dbContext
                .ConstructionCompanies
                .Include(c => c.Address)
                .Include(c => c.CompanyOwner)
                .Include(c => c.Employees)
                .FirstOrDefault(c => c.Id == id);

            if(constructionCompany is null)
            {
                return NotFound();
            }

            var constructionCompanyDto = mapper.Map<ConstructionCompanyDto>(constructionCompany);

            return Ok(constructionCompanyDto);
        }
    }
}
