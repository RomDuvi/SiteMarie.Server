using ServiceStack.DataAnnotations;

namespace SiteMarie.Server.API.Client.Database
{
    [Alias("users")]
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        
    }
}