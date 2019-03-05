using System;
using Microsoft.AspNetCore.Mvc;
using SiteMarie.Server.API.Client.Database;
using SiteMarie.Server.API.Client.Interfaces;

namespace SiteMarie.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private ICommandRepository Repository { get; set; }

        public CommandController(ICommandRepository repository)
        {
            Repository = repository;
        }

        #region GET
        [HttpGet]
        public IActionResult Get()
        {
            var commands = Repository.GetAll();
            return Ok(commands);
        }

        [HttpGet]
        [Route("{commandId}")]
        public IActionResult GetById(Guid commandId)
        {
            var res = Repository.GetById(commandId);
            return Ok(res);
        }
        #endregion
        #region POST
        [HttpPost]
        public IActionResult SaveCommand([FromBody]Command command)
        {
            var res = Repository.Add(command);
            return Ok(res);
        }
        #endregion
    }
}