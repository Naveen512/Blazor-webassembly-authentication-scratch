using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWasm.JwtAuthLearning.Services
{
    public class TodoService:ITodoService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenManagerService _tokenManagerService;

        public TodoService(HttpClient httpClient,
            ITokenManagerService tokenManagerService)
        {
            _httpClient = httpClient;
            _tokenManagerService = tokenManagerService;
        }

        public async Task<List<string>> GetTodos()
        {
            string token = await _tokenManagerService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return await _httpClient.GetFromJsonAsync<List<string>>("/test/todos");
        }
    }
}
