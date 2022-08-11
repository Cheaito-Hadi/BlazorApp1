using System.Data;
using System.Text;
using BlazorApp1.Data;
using BlazorApp1.IServices;
using Dapper;
using BlazorApp1.Services;
using MySql.Data.MySqlClient;
using MetroFramework;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace BlazorApp1.Services
{
    public class UserServices : IUserService
    {


        public IConfiguration _configuration { get; }
        public string _connectionString { get; }
        public IEmailService _email { get; }

        public UserServices(IConfiguration configuration, IEmailService email)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
            _email = email;
        }

        public async Task<IEnumerable<LoginDto>> Login(LoginDto user)
        {
            IEnumerable<LoginDto> userEntries;
            var query = "SELECT * FROM `people` WHERE User=@User AND Password=@Password";
            var parameters = new DynamicParameters();
            parameters.Add("User", user.User, DbType.String);
            parameters.Add("Password", user.Password, DbType.String);



            using var conn = new MySqlConnection(_connectionString);
            userEntries = await conn.QueryAsync<LoginDto>(query, parameters);
            if (userEntries == null)
            {
                return userEntries;
            }

            return userEntries;
        }

        public async Task<bool> SignUp(UserDto user)
        {
            var password = CreatePassword(10);
            var query = "INSERT INTO `people` (User, Password) VALUES (@User, @Password);SELECT LAST_INSERT_ID();";
            var parameters = new DynamicParameters();
            parameters.Add("User", user.User, DbType.String);
            parameters.Add("Password", password, DbType.String);

            using var conn = new MySqlConnection(_connectionString);
            await conn.ExecuteAsync(query, parameters);
            EmailDto email = new EmailDto();
            email.To = user.User;
            email.Subject = "Signup password";
            email.Body = password;
            _email.SendEmail(email);


            return true;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var parameters = new DynamicParameters();
            var query = "SELECT * FROM `people` Where Id=@Id";
            parameters.Add("Id", id, DbType.Int32);
            UserDto user = new UserDto();

            using (var conn = new MySqlConnection(_connectionString))
            {

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    user = await conn.QueryFirstOrDefaultAsync<UserDto>(query, parameters);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
                if (true)
                {

                }
            }
            return user;
        }




        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
