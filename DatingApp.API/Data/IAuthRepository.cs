using System.Threading.Tasks;
using DatingApp.API.Modules;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username); // check if the username is available
    }
}