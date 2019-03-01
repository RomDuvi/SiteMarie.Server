using System;
using ServiceStack.DataAnnotations;

namespace SiteMarie.Server.API.Client.Database
{
    [Alias("pictures_categories")]
    public class PictureCategory
    {
        public Guid Id { get; set; }
        [ForeignKey(typeof(Picture))]
        public Guid PictureId { get; set; }
        [ForeignKey(typeof(Category))]
        public Guid CategoryId { get; set; }
    }
}