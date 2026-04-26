using ZestTechicalAssignment.Business.Request.StudentRequest;
using ZestTechicalAssignment.Business.Response.StudentRes;
using ZestTechnicalAssignment.Domain.Entities;
using ZestTechnicalAssignment.Shared.ApiResponseModel;

namespace ZestTechicalAssignment.Business.Interfaces
{
    public interface IStudentRepositories
    {
         Task<(int, List<Student>)> GetAllStudentListAsync(GetAllStudentRequest request);
    }
}
