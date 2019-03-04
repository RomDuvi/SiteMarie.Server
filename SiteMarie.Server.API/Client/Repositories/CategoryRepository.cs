using System;
using System.Linq;
using System.Collections.Generic;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SiteMarie.Server.API.Client.Database;
using SiteMarie.Server.API.Client.Interfaces;
using System.IO;

namespace SiteMarie.Server.API.Client.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public override Category Add(Category category) 
        {
            category.Id = Guid.NewGuid();
            var ext = category.FileType.Split("/");
            category.FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", category.Name + "." + ext.Last());
            using(var outStream = File.Create(category.FilePath))
            {
                category.File.CopyTo(outStream);
            }
            return base.Add(category);
        }

        public override Category Update(Category category)
        {
            if(!string.IsNullOrEmpty(category.FilePath))
            {
                File.Delete(category.FilePath);
            }
            var ext = category.FileType.Split("/");
            category.FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", category.Name + "." + ext.Last());
            using(var outStream = File.Create(category.FilePath))
            {
                category.File.CopyTo(outStream);
            }
            return base.Update(category);
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
                if(!string.IsNullOrEmpty(category.FilePath))
                {
                    File.Delete(category.FilePath);
                }
                connection.DeleteById<Category>(category.Id);
            }
        }

        public byte[] GetCategoryFile(Guid categoryId)
        {
            var category = GetById(categoryId);
            if(category == null)
            {
                throw new ArgumentException("Can't find picture");
            }
            if(string.IsNullOrEmpty(category.FilePath))
            {
                return new byte[0];
            }
            return File.ReadAllBytes(category.FilePath);
        }      
    }
}