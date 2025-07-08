using System.Threading.Tasks;

namespace ChietaApp.Services
{
    public interface IAuthService
    {
        Task<bool> Login(string userNameOrEmailAddress, string password);
        
    }
}
