using ZestTechnicalAssignment.App.Models;
using ZestTechnicalAssignment.App.ResponseModel;

namespace ZestTechnicalAssignment.App.InterFacess
{
    public interface IAuthServices
    {
        Task<Result<string>> LoginAccount(LoginDto loginDto);
        Task<Result<string>> Register(RegistrationDto registerDto);
    }
}
