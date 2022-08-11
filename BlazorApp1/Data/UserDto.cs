using System.ComponentModel.DataAnnotations;


namespace BlazorApp1.Data
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="Enter a Valid Email")]
        public string User { get; set; }       
        public string Password { get; set; }
        
        public bool IsUpdate { get; set; } = false;
    }
}
