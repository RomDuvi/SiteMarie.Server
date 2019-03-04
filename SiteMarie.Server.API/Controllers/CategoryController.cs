using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpGet]
        [Route("file/{categoryId}")]
        public IActionResult GetCategoryFile(Guid categoryId)
        {
            var res = Repository.GetCategoryFile(categoryId);

            return Ok(res);
        }
        #endregion        
        #region POST
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Add() 
        {
            var files = Request.Form.Files;
            var file = files[0];
            
            var category = JsonConvert.DeserializeObject<Category>(Request.Form["category"]);
            category.File = file;
            var c = Repository.Add(category);
            return Ok(c);    
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
        public IActionResult UpdateCategory()
        {
            var files = Request.Form.Files;
            var file = files[0];
            
            var category = JsonConvert.DeserializeObject<Category>(Request.Form["category"]);
            category.File = file;
            var c = Repository.Update(category);
            return Ok(c);
        }
        #endregion
    }
}