using ZestTechnicalAssignment.App.InterFacess;
using ZestTechnicalAssignment.App.Models;
using ZestTechnicalAssignment.App.ResponseModel;

namespace ZestTechnicalAssignment.App.Servicess
{
    public class AuthServices(IApiServices apiService) : IAuthServices
    {
        public async Task<Result<string>> LoginAccount(LoginDto loginDto)
        {
            Result<string> result = await apiService.PostAsync<string>("Auth/Login", loginDto);
            return result;
        }

        public async Task<Result<string>> Register(RegistrationDto registerDto)
        {
            Result<string> result = await apiService.PostAsync<string>("Auth/Register", registerDto);
            return result;
        }
    }
}
