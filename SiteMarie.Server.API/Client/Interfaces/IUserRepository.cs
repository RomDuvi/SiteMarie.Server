using SiteMarie.Server.API.Client.Database;

namespace SiteMarie.Server.API.Client.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
         User Login(User user);
         User GetUserByUsername(string username);
    }
}