using ServiceStack.Data;
using System;
using SiteMarie.Server.API.Client.Database;
using SiteMarie.Server.API.Client.Interfaces;
using ServiceStack.OrmLite;

namespace SiteMarie.Server.API.Client.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public override User Add(User user)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;
            return base.Add(user);
        }

        public User Login(User user)
        {
            var existingUser = GetUserByUsername(user.Username);
            if(existingUser == null)
            {
                throw new ArgumentNullException("User does not exists!");
            }
            if(!BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password))
            {
                throw new ArgumentException("Wrong password!");
            }
            return existingUser;            
        }

        public User GetUserByUsername(string username)
        {
            using (var connection = ConnectionFactory.Open())
            {
                return connection.SingleWhere<User>("Username",username);
            }
        }
    }
}