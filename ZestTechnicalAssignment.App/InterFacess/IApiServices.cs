using ZestTechnicalAssignment.App.ResponseModel;

namespace ZestTechnicalAssignment.App.InterFacess
{
    public interface IApiServices
    {
        Task<Result<T>> GetAsync<T>(string endpoint);
        Task<Result<T>> PostAsync<T>(string endpoint, object data);
        Task<Result<T>> PutAsync<T>(string endpoint, object data);
        Task<Result<T>> DeleteAsync<T>(string endpoint, Guid id);
    }
}
