using Blazored.LocalStorage;
using System.Net.Http.Json;
using ZestTechnicalAssignment.App.InterFacess;
using ZestTechnicalAssignment.App.ResponseModel;

namespace ZestTechnicalAssignment.App.Servicess
{
    public class ApiServices(HttpClient httpClient , ILocalStorageService localStorageService): IApiServices
    {
        public async Task<Result<T>> DeleteAsync<T>(string endpoint, Guid id)
        {
            try
            {
                await ApplyHeader();
                var response = await httpClient.DeleteAsync(endpoint + "/" + id);
                return await ProcessResponse<T>(response);
            }
            catch (Exception ex)
            {
                return Result<T>.Failure(ex.Message);
            }
        }

        public async Task<Result<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                await ApplyHeader();
                HttpResponseMessage response = await httpClient.GetAsync(endpoint);
                return await ProcessResponse<T>(response);
            }
            catch (Exception ex)
            {
                return Result<T>.Failure(ex.Message);
            }
        }

        public async Task<Result<T>> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                await ApplyHeader();
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(endpoint, data);
                return await ProcessResponse<T>(response);
            }
            catch (Exception ex)
            {
                return Result<T>.Failure($"{ex.Message}");
            }
        }

        public async Task<Result<T>> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                await ApplyHeader();
                HttpResponseMessage response = await httpClient.PutAsJsonAsync(endpoint, data);
                return await ProcessResponse<T>(response);
            }
            catch (Exception ex)
            {
                return Result<T>.Failure(ex.Message);
            }
        }
        private async Task ApplyHeader()
        {
            var token = await localStorageService.GetItemAsync<string>("token");

            if (!string.IsNullOrWhiteSpace(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
        private async Task<Result<T>> ProcessResponse<T>(HttpResponseMessage response)
        {
            try
            {
                Result<T>? result = await response.Content.ReadFromJsonAsync<Result<T>>();
                return result ?? Result<T>.Failure("Invalid response format");
            }
            catch (Exception ex)
            {
                return Result<T>.Failure(ex.Message);
            }
        }
    }
}
