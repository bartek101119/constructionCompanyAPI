using AutoMapper;
using constructionCompanyAPI.Entities;
using constructionCompanyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI
{
    public class ConstructionCompanyMappingDto : Profile
    {
        public ConstructionCompanyMappingDto()
        {
            CreateMap<ConstructionCompany, ConstructionCompanyDto>()
                .ForMember(m => m.Voivodeship, f => f.MapFrom(c => c.Address.Voivodeship))
                .ForMember(m => m.City, f => f.MapFrom(c => c.Address.City))
                .ForMember(m => m.Street, f => f.MapFrom(c => c.Address.Street))
                .ForMember(m => m.PostalCode, f => f.MapFrom(c => c.Address.PostalCode))
                .ForMember(m => m.FullName, f => f.MapFrom(c => c.CompanyOwner.FullName));

            CreateMap<Employee, EmployeeDto>();



        }
    }
}
