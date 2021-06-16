using AutoMapper;
using constructionCompanyAPI.Entities;
using constructionCompanyAPI.Exceptions;
using constructionCompanyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Services
{
    public interface IEmployeeService
    {
        int Post(int constructionCompanyId, CreateEmployeeDto dto);
        EmployeeDto GetById(int constructionCompanyId, int employeeId);
        List<EmployeeDto> GetAll(int constructionCompanyId);
        void RemoveAll(int constructionCompanyId);
        void Put(UpdateEmployeeDto dto, int constructionCompanyId, int employeeId);
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
        public void Put(UpdateEmployeeDto dto, int constructionCompanyId, int employeeId)
        {
            var constructionCompany = ConstructionCompanyById(constructionCompanyId);

            var employee = dbContext.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee is null || employee.Id != employeeId)
                throw new NotFoundException("Employee not found");

            employee.FullName = dto.FullName;
            employee.Specialization = dto.Specialization;

            dbContext.SaveChanges();


        }
        public void RemoveAll(int constructionCompanyId)
        {
            var constructionCompany = ConstructionCompanyById(constructionCompanyId);

            dbContext.RemoveRange(constructionCompany.Employees);
            dbContext.SaveChanges();
        }
        public int Post(int constructionCompanyId, CreateEmployeeDto dto)
        {
            var constructionCompany = ConstructionCompanyById(constructionCompanyId);

            var newEmployee = mapper.Map<Employee>(dto);

            newEmployee.ConstructionCompanyId = constructionCompanyId;

            dbContext.Employees.Add(newEmployee);
            dbContext.SaveChanges();

            return newEmployee.Id;
        }

        public EmployeeDto GetById(int constructionCompanyId, int employeeId)
        {
            var constructionCompany = ConstructionCompanyById(constructionCompanyId);

            var employee = dbContext.Employees.FirstOrDefault(e => e.Id == employeeId);
            if(employee is null || employee.Id != employeeId)
                throw new NotFoundException("Employee not found");

            var employeeDto = mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public List<EmployeeDto> GetAll(int constructionCompanyId)
        {
            var constructionCompany = ConstructionCompanyById(constructionCompanyId);

            var employeeDtos = mapper.Map<List<EmployeeDto>>(constructionCompany.Employees);

            return employeeDtos;
        }

        private ConstructionCompany ConstructionCompanyById(int constructionCompanyId)
        {
            var constructionCompany = dbContext.ConstructionCompanies.Include(e => e.Employees).FirstOrDefault(c => c.Id == constructionCompanyId);
            if (constructionCompany is null)
                throw new NotFoundException("Construction Company not found");

            return constructionCompany;
        }
    }
}
