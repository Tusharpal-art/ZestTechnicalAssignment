using ZestTechicalAssignment.Business.Request.AuthRequest;
using ZestTechnicalAssignment.Domain.Entities;
using ZestTechnicalAssignment.Shared.ApiResponseModel;

namespace ZestTechicalAssignment.Business.Interfaces
{
    public interface IAuthRepositories
    {
        Task<Result<string>> Login(LoginRequest request);
        Task<Result<string>> Registration(RegistrationRequest request);
        Task<User?> GetCurrentUser ();
    }
}
