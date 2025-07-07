using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChietaApp.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public int ExpireInSeconds { get; set; }
        public UserDto User { get; set; }
    }

    public class UserDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
    }
}
