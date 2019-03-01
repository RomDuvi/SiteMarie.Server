using System;
using ServiceStack.DataAnnotations;

namespace SiteMarie.Server.API.Client.Database
{
    public class BaseModel
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}