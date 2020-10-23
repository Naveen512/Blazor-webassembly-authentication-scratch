using System.Threading.Tasks;

namespace BlazorWasm.JwtAuthLearning.Services
{
    public interface ITokenManagerService
    {
        Task<string> GetTokenAsync();
    }
}
