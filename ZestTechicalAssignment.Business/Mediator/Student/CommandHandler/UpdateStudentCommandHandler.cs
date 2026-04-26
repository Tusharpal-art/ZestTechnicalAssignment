using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ZestTechicalAssignment.Business.Interfaces;
using ZestTechicalAssignment.Business.Request.StudentRequest;
using ZestTechicalAssignment.Business.Response.StudentRes;
using ZestTechnicalAssignment.Shared.ApiResponseModel;

namespace ZestTechicalAssignment.Business.Mediator.Student.CommandHandler
{
    public class UpdateStudentCommandHandler(IAuthRepositories auth,IUnitOfRepositories unitOfRepositories ,IMapper mapper) : IRequestHandler<UpdateStudentRequest, Result<StudentResponse>>
    {
        public async Task<Result<StudentResponse>> Handle(UpdateStudentRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await auth.GetCurrentUser();
            if (currentUser == null) return Result<StudentResponse>.Failure("User Not Exists");

            var isStudentExits = await unitOfRepositories.GetRepository<ZestTechnicalAssignment.Domain.Entities.Student>().GetById(request.Id);

            if(isStudentExits==null)
            {
               return Result<StudentResponse>.Failure("The Updated Student Not Exist");
            }

            isStudentExits.Name = request.Name ?? isStudentExits.Name;
            isStudentExits.Age = request.Age ?? isStudentExits.Age;
            isStudentExits.Course = request.Course ?? isStudentExits.Course;
            isStudentExits.Email = request.Email ?? isStudentExits.Email;

            var response = await unitOfRepositories.GetRepository<ZestTechnicalAssignment.Domain.Entities.Student>().Update(isStudentExits);
            await unitOfRepositories.SaveChangesAsync(cancellationToken);

            return Result<StudentResponse>.Successs(mapper.Map<StudentResponse>(isStudentExits));

        }
    }
}
