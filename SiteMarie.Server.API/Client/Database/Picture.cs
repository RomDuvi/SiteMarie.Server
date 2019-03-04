using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using ServiceStack.DataAnnotations;

namespace SiteMarie.Server.API.Client.Database
{
    [Alias("pictures")]
    public class Picture : BaseModel
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string ThumbPath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double Price { get; set; }
        [Reference]
        public List<PictureCategory> PictureCategories{ get; set;} 
        [Ignore]
        public string FileName { get; set; }
        [Ignore]
        public IFormFile File { get; set; }
        
    }
}