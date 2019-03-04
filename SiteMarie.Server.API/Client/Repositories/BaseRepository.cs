using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using SiteMarie.Server.API.Client.Database;
using SiteMarie.Server.API.Client.Interfaces;
using MySql.Data.MySqlClient;
using Dapper;
using System.Linq;
using ServiceStack.OrmLite;
using ServiceStack.Data;

namespace SiteMarie.Server.API.Client.Repositories
{
    public abstract class BaseRepository<T> where T : BaseModel
    {
        public IDbConnectionFactory ConnectionFactory { get; set; }
        public BaseRepository(IDbConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        public virtual T Add(T entity)
        {
            using (var connection = ConnectionFactory.Open())
            {
                if(entity.Id == null || entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                connection.Save(entity, true);
                return entity;
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            using (var connection = ConnectionFactory.Open())
            {
                return connection.LoadSelect<T>();
            }
        }

        public T GetById(Guid id)
        {
            using (var connection = ConnectionFactory.Open())
            {
                return connection.LoadSingleById<T>(id);
            }
        }

        public virtual void Remove(T entity)
        {
            using (var connection = ConnectionFactory.Open())
            {
                connection.DeleteById<T>(entity.Id);
            }
        }

        public virtual T Update(T entity)
        {
            using (var connection = ConnectionFactory.Open())
            {
                entity.UpdatedAt = DateTime.Now;
                connection.Save<T>(entity, true);
                return entity;
            }
        }
    }
}