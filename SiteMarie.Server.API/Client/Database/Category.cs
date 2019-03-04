using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using ServiceStack.DataAnnotations;

namespace SiteMarie.Server.API.Client.Database
{
    [Alias("categories")]
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        [Ignore]
        public IFormFile File { get; set; }

        [Reference]
        public List<PictureCategory> PictureCategories { get; set; }

    }
}