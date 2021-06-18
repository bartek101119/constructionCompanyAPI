using AutoMapper;
using constructionCompanyAPI.Entities;
using constructionCompanyAPI.Models;
using constructionCompanyAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Controllers
{
    [Route("/api/constructionCompany")]
    [ApiController]
    [Authorize]
    public class ConstructionCompanyController : ControllerBase
    {
        private readonly IConstructionCompanyService service;

        public ConstructionCompanyController(IConstructionCompanyService service)
        {
            this.service = service;
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody]UpdateConstructionCompanyDto dto, [FromRoute]int id)
        {
            service.Put(dto, id, User);

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            service.Delete(id, User);

            return NoContent();

        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult CreateConstructionCompany([FromBody] CreateConstructionCompanyDto dto)
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = service.Create(dto, userId);

            return Created($"api/constructionCompany/{id}", null);
        }

        [HttpGet]
        [Authorize(Policy = "Atleast18")]
        public ActionResult<IEnumerable<ConstructionCompanyDto>> GetAll()
        {
            var constructionCompaniesDtos = service.GetAll();

            return Ok(constructionCompaniesDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ConstructionCompanyDto> Get([FromRoute]int id)
        {
            var constructionCompanyDto = service.GetById(id);

            return Ok(constructionCompanyDto);
        }
    }
}
