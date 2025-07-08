using System.ComponentModel.DataAnnotations;

namespace ChietaApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username or Email is required")]
        public string UserNameOrEmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}

