using System.Threading.Tasks;

namespace EMSstartbit.TokenAuthentication
{
    public interface ITokenManager
    {
        object Ecoding { get; }

        Task<string> Authenticate(int username, string password);
        Task<string> NewToken(string username);
        Task<string> VerifyToken(string token);
    }
}