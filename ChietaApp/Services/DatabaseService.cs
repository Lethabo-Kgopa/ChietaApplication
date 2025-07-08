using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Maui.Storage;


namespace ChietaApp.Services
{
    public class DatabaseService
    {
        private List<UserData> _users = new();

        public UserData? LoggedInUser { get; private set; }

        /// <summary>
        /// Loads user data from the packaged users.json file.
        /// </summary>
        public async Task InitializeAsync()
        {
            try
            {
                // Load users.json from app package root (configured via .csproj LogicalName="users.json")
                using var fileStream = await FileSystem.OpenAppPackageFileAsync("users.json");
                using var reader = new StreamReader(fileStream);
                var json = await reader.ReadToEndAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                _users = JsonSerializer.Deserialize<List<UserData>>(json, options) ?? new List<UserData>();
            }
            catch (Exception ex)
            {
                // Handle file not found or JSON parsing errors
                Console.WriteLine($"Error loading user data: {ex.Message}");
                _users = new List<UserData>();
            }
        }

        /// <summary>
        /// Attempts to log in with the given username/email and password.
        /// </summary>
        public string Login(string usernameOrEmail, string password)
        {
            if (_users == null || !_users.Any())
            {
                Console.WriteLine("User data not loaded. Did you forget to call InitializeAsync?");
                return string.Empty;
            }

            var user = _users.FirstOrDefault(u =>
                string.Equals(u.UserNameOrEmailAddress, usernameOrEmail, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

            if (user != null)
            {
                LoggedInUser = user;
                return user.FullName;
            }

            return string.Empty;
        }
    }

    public class UserData
    {
        public string UserNameOrEmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string SDLNO { get; set; } = string.Empty;
        public string OrganisationalName { get; set; } = string.Empty;
    }
}
