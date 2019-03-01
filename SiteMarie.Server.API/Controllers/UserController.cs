using System;
using Microsoft.AspNetCore.Mvc;
using SiteMarie.Server.API.Client.Database;
using SiteMarie.Server.API.Client.Interfaces;

namespace SiteMarie.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository Repository { get; set; }
        public UserController(IUserRepository repository)
        {
            Repository = repository;
        }

        #region GET
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = Repository.GetAll();
            return Ok(users);
        }

        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetById(Guid userId)
        {
            var user = Repository.GetById(userId);
            return Ok(user);
        }
        #endregion

        #region POST
        [HttpPost]
        public IActionResult SaveUser([FromBody] User u)
        {
            var user = Repository.Add(u);
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] User u)
        {
            var user = Repository.Login(u);
            return Ok(user);
        }
        #endregion
        
        #region PUT
        [HttpPut]
        public IActionResult UpdateUser([FromBody]User u)
        {
            var user = Repository.Update(u);
            return Ok(user);
        }
        #endregion
    }
}