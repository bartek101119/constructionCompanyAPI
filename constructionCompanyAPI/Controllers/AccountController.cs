using constructionCompanyAPI.Models;
using constructionCompanyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpPost("register")]
        public ActionResult Register([FromBody]RegisterUserDto dto)
        {
            accountService.Register(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto)
        {
            var token = accountService.GenerateJwt(dto);

            return Ok(token);
        }
    }
}
