using System;
using Microsoft.AspNetCore.Mvc;
using SiteMarie.Server.API.Client.Database;
using SiteMarie.Server.API.Client.Interfaces;

namespace SiteMarie.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private ICategoryRepository Repository{get;set;}
        public CategoryController(ICategoryRepository repository){
            Repository = repository;
        }

        #region GET
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = Repository.GetAll();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{categoryId}")]
        public IActionResult GetById(Guid categoryId)
        {
            var picture = Repository.GetById(categoryId);
            return Ok(picture);
        }
        #endregion        
        #region POST
        [HttpPost]
        public IActionResult Add([FromBody] Category c) 
        {
            var category = Repository.Add(c);
            return Ok(category);    
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromBody] Category category)
        {
            Repository.Remove(category);
            return Ok(category);
        }
        
        #endregion
        #region PUT
        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            var cat = Repository.Update(category);
            return Ok(cat);
        }
        #endregion
    }
}