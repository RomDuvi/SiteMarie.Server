using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace SiteMarie.Server.API.Client.Database
{
    [Alias("categories")]
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Reference]
        public List<PictureCategory> PictureCategories { get; set; }

    }
}