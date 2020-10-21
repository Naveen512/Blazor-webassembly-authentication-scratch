using BlazorWasm.JwtAuthLearning.Models;
using System.Threading.Tasks;

namespace BlazorWasm.JwtAuthLearning.Services
{
    #region Mock Claims
    //public interface IAccountService
    //{
    //    bool Login();
    //    bool Logout();
    //} 
    #endregion

    public interface IAccountService
    {
        Task<bool> LoginAsync(LoginModel model);
        Task<bool> LogoutAsync();
    }
}
