using System;
using System.Collections.Generic;
using SiteMarie.Server.API.Client.Database;

namespace SiteMarie.Server.API.Client.Interfaces
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        T Add(T entity);
        void Remove(T entity);
        T Update(T entity);    
    }
}