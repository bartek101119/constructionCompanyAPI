using AutoMapper;
using constructionCompanyAPI.Entities;
using constructionCompanyAPI.Models;
using constructionCompanyAPI.Services;
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
        private readonly IConstructionCompanyService service;

        public ConstructionCompanyController(IConstructionCompanyService service)
        {
            this.service = service;
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody]UpdateConstructionCompanyDto dto, [FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = service.Put(dto, id);

            if (isUpdated)
            {
                return Ok();
            }

            return NotFound();
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var isDeleted = service.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateConstructionCompany([FromBody] CreateConstructionCompanyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = service.Create(dto);

            return Created($"api/constructionCompany/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ConstructionCompanyDto>> GetAll()
        {
            var constructionCompaniesDtos = service.GetAll();

            return Ok(constructionCompaniesDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ConstructionCompanyDto> Get([FromRoute]int id)
        {
            var constructionCompanyDto = service.GetById(id);

            if(constructionCompanyDto is null)
            {
                return NotFound();
            }

            return Ok(constructionCompanyDto);
        }
    }
}
