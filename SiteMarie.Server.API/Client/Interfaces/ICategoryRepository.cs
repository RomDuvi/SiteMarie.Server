using System;
using SiteMarie.Server.API.Client.Database;

namespace SiteMarie.Server.API.Client.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
         byte[] GetCategoryFile(Guid categoryId);
    }
}