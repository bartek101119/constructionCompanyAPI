using AutoMapper;
using constructionCompanyAPI.Entities;
using constructionCompanyAPI.Exceptions;
using constructionCompanyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Services
{
    public interface IEmployeeService
    {
        int Post(int constructionCompanyId, CreateEmployeeDto dto);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly ConstructionCompanyDbContext dbContext;
        private readonly IMapper mapper;

        public EmployeeService(ConstructionCompanyDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public int Post(int constructionCompanyId, CreateEmployeeDto dto)
        {
            var constructionCompany = dbContext.ConstructionCompanies.FirstOrDefault(c => c.Id == constructionCompanyId);
            if (constructionCompany is null)
                throw new NotFoundException("Construction Company not found");

            var newEmployee = mapper.Map<Employee>(dto);

            newEmployee.ConstructionCompanyId = constructionCompanyId;

            dbContext.Employees.Add(newEmployee);
            dbContext.SaveChanges();

            return newEmployee.Id;
        }
    }
}
