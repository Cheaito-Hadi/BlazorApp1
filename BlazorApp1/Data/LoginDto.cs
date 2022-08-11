using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Data
{
    public class LoginDto
    {

        [Required(ErrorMessage ="EMAIL IS REQUIRED")]
        public string User { get; set; }
        [Required(ErrorMessage = "PASSWORD IS REQUIRED")]
        public string Password { get; set; }
    }
}
