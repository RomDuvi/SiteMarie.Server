using System;
using System.Linq;
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
        public override void Remove(Category c)
        {
            var category = GetById(c.Id);
            if(category == null)
            {
                throw new ArgumentException("Can't find category to delete");
            }
            if(category.PictureCategories.Any())
            {
                throw new InvalidOperationException("Can't delete category beacause they are pictures in it!");
            }
            using (var connection = ConnectionFactory.Open())
            {
                connection.DeleteById<Category>(category.Id);
            }
        }      
    }
}