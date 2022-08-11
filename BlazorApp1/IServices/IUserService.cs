using BlazorApp1.Data;

namespace BlazorApp1.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<LoginDto>> Login(LoginDto user);
        Task<bool> SignUp(UserDto user);
        Task<UserDto> GetUserById(int id);
        
    }
}