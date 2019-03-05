using System;
using System.Collections.Generic;
using ServiceStack.Data;
using SiteMarie.Server.API.Client.Database;
using SiteMarie.Server.API.Client.Interfaces;

namespace SiteMarie.Server.API.Client.Repositories
{
    public class CommandRepository : BaseRepository<Command>, ICommandRepository
    {
        public CommandRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}