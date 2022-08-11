namespace BlazorApp1
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
    }
}
