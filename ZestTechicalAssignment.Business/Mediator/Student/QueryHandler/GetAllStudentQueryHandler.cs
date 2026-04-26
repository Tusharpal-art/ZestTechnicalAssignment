using AutoMapper;
using MediatR;
using ZestTechicalAssignment.Business.Interfaces;
using ZestTechicalAssignment.Business.Request.StudentRequest;
using ZestTechicalAssignment.Business.Response.StudentRes;
using ZestTechnicalAssignment.Shared.ApiResponseModel;

namespace ZestTechicalAssignment.Business.Mediator.Student.QueryHandler
{
    public class GetAllStudentQueryHandler(IMapper mapper, IUnitOfRepositories unitOfRepositories) : IRequestHandler<GetAllStudentRequest, Result<Tuple<int, List<StudentResponse>>>>
    {
        public async Task<Result<Tuple<int, List<StudentResponse>>>> Handle(GetAllStudentRequest request, CancellationToken cancellationToken)
        {
            var listOfValue = await unitOfRepositories.StudentRepo.GetAllStudentListAsync(request);
            List<StudentResponse> studentList = mapper.Map<List<StudentResponse>>(listOfValue.Item2);
            return Result<Tuple<int, List<StudentResponse>>>.Successs(new Tuple<int, List<StudentResponse>>(listOfValue.Item1,studentList));
        } 
    }
}
