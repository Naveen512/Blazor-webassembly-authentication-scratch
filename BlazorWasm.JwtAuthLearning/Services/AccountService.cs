using Blazored.LocalStorage;
using BlazorWasm.JwtAuthLearning.Auth;
using BlazorWasm.JwtAuthLearning.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWasm.JwtAuthLearning.Services
{
    #region Mock Claims
    //public class AccountService : IAccountService
    //{
    //    private readonly AuthenticationStateProvider _authenticationStateProvider;
    //    public AccountService(AuthenticationStateProvider authenticationStateProvider)
    //    {
    //        _authenticationStateProvider = authenticationStateProvider;
    //    }
    //    public bool Login()
    //    {
    //        (_authenticationStateProvider as CustomAuthenticationProvider).LoginNotify();
    //        return true;
    //    }

    //    public bool Logout()
    //    {
    //        (_authenticationStateProvider as CustomAuthenticationProvider).LogoutNotify();
    //        return true;
    //    }
    //} 
    #endregion
    #region token
    public class AccountService : IAccountService
    {
        private readonly AuthenticationStateProvider _customAuthenticationProvider;
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;
        public AccountService(ILocalStorageService localStorageService,
            AuthenticationStateProvider customAuthenticationProvider,
            HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _customAuthenticationProvider = customAuthenticationProvider;
            _httpClient = httpClient;
        }
        public async Task<bool> LoginAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync<LoginModel>("/account/login-token", model);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            AuthResponse authData = await response.Content.ReadFromJsonAsync<AuthResponse>();
            await _localStorageService.SetItemAsync("token", authData.Token);
            await _localStorageService.SetItemAsync("refreshToken", authData.RefreshToken);
            (_customAuthenticationProvider as CustomAuthenticationProvider).Notify();
            return true;
        }

        public async Task<bool> LogoutAsync()
        {
            await _localStorageService.RemoveItemAsync("token");
            (_customAuthenticationProvider as CustomAuthenticationProvider).Notify();
            return true;
        }
    }
    #endregion
}
