using System.Data;
using System.Text;
using BlazorApp1.Data;
using BlazorApp1.IServices;
using Dapper;
using MySql.Data.MySqlClient;

namespace BlazorApp1.Services
{
    public class ApiServices : IApiService
    {
        public IConfiguration _configuration { get; }
        public string _connectionString { get; }

        public ApiServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public async Task<bool> DeleteApi(int id)
        {
            var query = "DELETE FROM `apis` WHERE ApiId = @ApiId";
            var parameters = new DynamicParameters();
            parameters.Add("ApiId", id, DbType.Int32);

            using (var conn = new MySqlConnection(_connectionString))
            {

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(query, parameters);
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
            }
            return true;
        }

        public async Task<IEnumerable<ApiDto>> GetApi()
        {
            IEnumerable<ApiDto> apiEntries;
            var query = "SELECT * FROM `apis`";
            using (var conn = new MySqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    apiEntries = await conn.QueryAsync<ApiDto>(query);
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
            }
            return apiEntries;
        }

        public async Task<ApiDto> GetApiById(int id)
        {
            var parameters = new DynamicParameters();
            var query = "SELECT * FROM `apis` Where ApiId=@ApiId";
            parameters.Add("ApiId", id, DbType.Int32);
            ApiDto api = new ApiDto();

            using (var conn = new MySqlConnection(_connectionString))
            {

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    api = await conn.QueryFirstOrDefaultAsync<ApiDto>(query, parameters);
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
            }
            return api;
        }

        public async Task<bool> SaveApiDetails(ApiDto api)
        {
            var query = "INSERT INTO `apis` (ApiName, ApiKey) VALUES (@ApiName, @ApiKey);SELECT LAST_INSERT_ID();";
            var query2 = "UPDATE `apis` SET ApiName = @ApiName, ApiKey=@ApiKey  WHERE ApiId = @ApiId";
            var parameters = new DynamicParameters();
            parameters.Add("ApiName", api.ApiName, DbType.String);
            parameters.Add("ApiKey", CreateApi(15), DbType.String);

            using (var conn = new MySqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    if (api.IsUpdate)
                    {
                        parameters.Add("ApiId", api.ApiId, DbType.Int32);
                        await conn.ExecuteAsync(query2, parameters);
                    }
                    else
                        await conn.ExecuteAsync(query, parameters);


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
            }
            return true;
        }
       
        public string CreateApi(int length)
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
