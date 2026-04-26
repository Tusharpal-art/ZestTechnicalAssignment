using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ZestTechicalAssignment.Business.Interfaces;
using ZestTechicalAssignment.Business.Request.StudentRequest;
using ZestTechnicalAssignment.Domain;
using ZestTechicalAssignment.Business.Response.StudentRes;
using ZestTechnicalAssignment.Shared.ApiResponseModel;

namespace ZestTechicalAssignment.Business.Mediator.Student.CommandHandler
{
    public class AddStudentCommandHandler(IMapper mapper , IUnitOfRepositories unitOfRepositories,IAuthRepositories auth) : IRequestHandler<AddStudentRequest, Result<StudentResponse>>
    {
        public async Task<Result<StudentResponse>> Handle(AddStudentRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await auth.GetCurrentUser();
            if (currentUser == null) return Result<StudentResponse>.Failure("User Not Exists");


            var student = mapper.Map<ZestTechnicalAssignment.Domain.Entities.Student>(request);

            student.CreatedBy = currentUser;
            student.CreatedById = currentUser.Id;

           var entity =  await unitOfRepositories.GetRepository<ZestTechnicalAssignment.Domain.Entities.Student>().Add(student);

            await unitOfRepositories.SaveChangesAsync(cancellationToken);


            return Result<StudentResponse>.Successs(mapper.Map<StudentResponse>(student));

        }
    }
}
