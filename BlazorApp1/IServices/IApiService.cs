using BlazorApp1.Data;

namespace BlazorApp1.IServices
{
    public interface IApiService
    {
        Task<IEnumerable<ApiDto>> GetApi();
        Task<bool> SaveApiDetails(ApiDto api);
        Task<ApiDto> GetApiById(int id);
        Task<bool> DeleteApi(int id);

 
    }
}
