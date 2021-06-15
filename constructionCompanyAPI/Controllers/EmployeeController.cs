using constructionCompanyAPI.Models;
using constructionCompanyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Controllers
{
    [Route("api/constructionCompany/{constructionCompanyId}/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService service;

        public EmployeeController(IEmployeeService service)
        {
            this.service = service;
        }
        public ActionResult Post([FromRoute]int constructionCompanyId, [FromBody]CreateEmployeeDto dto)
        {
            var newEmployeeId = service.Post(constructionCompanyId, dto);

            return Created($"/api/constructionCompany/{constructionCompanyId}/{newEmployeeId}", null);

        }

        [HttpGet("{employeeId}")]
        public ActionResult<EmployeeDto> Get([FromRoute]int constructionCompanyId, [FromRoute]int employeeId)
        {
            EmployeeDto employee = service.GetById(constructionCompanyId, employeeId);

            return Ok(employee);
        }

        [HttpGet]
        public ActionResult<List<EmployeeDto>> GetAll([FromRoute] int constructionCompanyId)
        {
            var employees = service.GetAll(constructionCompanyId);

            return Ok(employees);
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int constructionCompanyId)
        {
            service.RemoveAll(constructionCompanyId);

            return NoContent();
        }
        
    }
}
