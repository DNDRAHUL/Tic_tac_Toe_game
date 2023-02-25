using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Policy;
using Tictoe.DAL;
using Tictoe.Repo;

namespace Tictoe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public IConfiguration configuration;
        public ManualDbContext manualdbcontext;
       

        public UserController(IConfiguration _configuration, ManualDbContext _manualdbcontext)
        {
            configuration = _configuration;
            manualdbcontext = _manualdbcontext;


        }

        [HttpGet]
        [Route("/user/get_users")]
        public IActionResult GetAllUser()
        {
            UserRepo repo = new UserRepo(configuration, manualdbcontext);
            var users = repo.GetAllUser();
            return Ok(users);
        }

    }
}
