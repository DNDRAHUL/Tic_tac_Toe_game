using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text.Json.Serialization;
using Tictoe.DAL;
using Tictoe.Helper;
using Tictoe.Model;
using Tictoe.Repo;
using static Tictoe.INPUTClass.InputClass;

namespace Tictoe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public IConfiguration configuration;
        public ManualDbContext manualdbcontext;
        private readonly AutoDbContext _dbContext;


        public UserController(IConfiguration _configuration, ManualDbContext _manualdbcontext, AutoDbContext dbContext)
        {
            configuration = _configuration;
            manualdbcontext = _manualdbcontext;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("/user/get_users")]
        public IActionResult GetAllUser()
        {
            UserRepo repo = new UserRepo(configuration, manualdbcontext);
            var users = repo.GetAllUser();
            return Ok(users);
        }

        [HttpPost]
        [Route("/user/register")]
        public IActionResult Register([FromBody] RegsiterIC obj)
        {
            User user = new User();

            user.first_name = obj.firstName;
            user.last_name=obj.lastName;
            user.email = obj.email;
            user.password = obj.password;
            user.Status_Code = true;
            user.Created_at = DateTime.Now;
            user.updated_at = DateTime.Now;
            user.Code = Guid.NewGuid();
            _dbContext.userDb.Add(user).State=EntityState.Added;
            _dbContext.SaveChanges();
            var data = new { message = "success", statusCode = 200 };
            var json = JsonConvert.SerializeObject(data);
            return Ok(json);
        }

        [HttpPut]
        [Route("/user/update")]
        public IActionResult UpdateUser([FromBody] RegsiterIC obj)
        {
            var data = new { message = "data updated successfully", statusCode = 200 };

            if (obj.email.IsNotNull())
            {
                User user = _dbContext.userDb.Where(x => x.Status_Code == true && x.email == obj.email).FirstOrDefault();

                if (user.IsNotNull())
                {
                    user.first_name = obj.firstName;
                    user.last_name = obj.lastName;
                    user.email = obj.email;
                    user.password = obj.password;
                    user.updated_at = DateTime.Now;
                    _dbContext.userDb.Add(user).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }
                else
                {
                    data = new { message = "Invalid email id", statusCode = 401 };
                }
            } else { data = new { message = "Email Id is required", statusCode = 401 }; }
           
           var json = JsonConvert.SerializeObject(data);
            return Ok(json);
        }

        [HttpPut]
        [Route("/user/signIn")]
        public IActionResult SignUser([FromBody] RegsiterIC obj)
        {
            var data = new { message = "Successfully SignedIn", statusCode = 200 };

            if (obj.email.IsNotNull() && obj.password.IsNotNull())
            {
                User user = _dbContext.userDb.Where(x => x.Status_Code == true && x.email == obj.email && x.password == obj.password).FirstOrDefault();

                if (user.IsNotNull())
                {
                    //user.first_name = obj.firstName;
                    //user.last_name = obj.lastName;
                    //user.email = obj.email;
                    //user.password = obj.password;
                    user.updated_at = DateTime.Now;
                    _dbContext.userDb.Add(user).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }
                else
                {
                    data = new { message = "Invalid email id or Password", statusCode = 401 };
                }
            }
            else { data= new { message = "Email Id and Password are required fields", statusCode = 401 }; }
           
            var json = JsonConvert.SerializeObject(data);
            return Ok(json);
        }

        [HttpPut]
        [Route("/user/forget_passord")]
        public IActionResult ForgetPassword([FromBody] RegsiterIC obj)
        {
            var data = new { message = "passord updated successfully", statusCode = 200 };

            if (obj.email.IsNotNull())
            {
                User user = _dbContext.userDb.Where(x => x.Status_Code == true && x.email == obj.email).FirstOrDefault();

                if (user.IsNotNull())
                {
                    //user.first_name = obj.firstName;
                    //user.last_name = obj.lastName;
                    //user.email = obj.email;
                    user.password = obj.password;
                    user.updated_at = DateTime.Now;
                    _dbContext.userDb.Add(user).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }
                else
                {
                    data = new { message = "Invalid email id", statusCode = 401 };
                }
            }
            else { data = new { message = "Email Id is required", statusCode = 401 }; }

            var json = JsonConvert.SerializeObject(data);
            return Ok(json);
        }


        [HttpPut]
        [Route("/user/logout")]
        public IActionResult logout([FromBody] RegsiterIC obj)
        {
            var data = new { message = "User logged out successfully", statusCode = 200 };

            if (obj.email.IsNotNull())
            {
                User user = _dbContext.userDb.Where(x => x.Status_Code == true && x.email == obj.email && x.password==obj.password).FirstOrDefault();

                if (user.IsNotNull())
                {
                    //user.first_name = obj.firstName;
                    //user.last_name = obj.lastName;
                    //user.email = obj.email;
                    //user.password = obj.password;
                    user.Status_Code = false;
                    user.updated_at = DateTime.Now;
                    _dbContext.userDb.Add(user).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }
                else
                {
                    data = new { message = "Invalid email id", statusCode = 401 };
                }
            }
            else { data = new { message = "Email Id is required", statusCode = 401 }; }

            var json = JsonConvert.SerializeObject(data);
            return Ok(json);
        }





    }
}
