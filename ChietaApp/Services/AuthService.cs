using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using ChietaApp.Services;


namespace ChietaApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5000/")
            };
        }

        public async Task<bool> Login(string userNameOrEmailAddress, string password)
        {
            try
            {
                var payload = new
                {
                    userNameOrEmailAddress,
                    password,
                    rememberClient = true
                };

                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/TokenAuth/Authenticate", content);

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Login Response: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    return false;
                }

                var result = JsonSerializer.Deserialize<LoginResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (!string.IsNullOrWhiteSpace(result?.AccessToken))
                {
                    await SecureStorage.SetAsync("auth_token", result.AccessToken);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }
    }

    public class LoginResponse
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}
