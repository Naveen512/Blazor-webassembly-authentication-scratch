using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWasm.JwtAuthLearning.Services
{
    public interface ITodoService
    {
        Task<List<string>> GetTodos();
    }
}
