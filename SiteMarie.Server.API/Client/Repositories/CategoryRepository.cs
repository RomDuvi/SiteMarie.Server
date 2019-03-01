using System.Collections.Generic;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SiteMarie.Server.API.Client.Database;
using SiteMarie.Server.API.Client.Interfaces;

namespace SiteMarie.Server.API.Client.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }      
    }
}