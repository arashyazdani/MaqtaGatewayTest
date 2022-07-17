using System.ComponentModel;

namespace API.Dtos
{
    [DisplayName("Login")]
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
