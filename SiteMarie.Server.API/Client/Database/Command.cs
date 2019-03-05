using System;
using ServiceStack.DataAnnotations;

namespace SiteMarie.Server.API.Client.Database
{
    [Alias("commands")]
    public class Command : BaseModel
    {
        [ForeignKey(typeof(Picture))]
        public Guid PictureId { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerLastName { get; set; }
        public string BuyerFirstName { get; set; }
        public string BuyerAddress { get; set; }
        public double Price { get; set; }
    }
}