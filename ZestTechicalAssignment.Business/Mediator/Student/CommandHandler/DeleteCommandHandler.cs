using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using ZestTechicalAssignment.Business.Interfaces;
using ZestTechicalAssignment.Business.Request.StudentRequest;
using ZestTechicalAssignment.Business.Response.StudentRes;
using ZestTechnicalAssignment.Domain.Entities;
using ZestTechnicalAssignment.Shared.ApiResponseModel;

namespace ZestTechicalAssignment.Business.Mediator.Student.CommandHandler
{
    public class DeleteCommandHandler(IUnitOfRepositories unitOfRepositories,IAuthRepositories auth,IMapper mapper,ILogger<DeleteCommandHandler> _logger) : IRequestHandler<DeleteStudentRequest, Result<StudentResponse>>
    {
        public async Task<Result<StudentResponse>> Handle(DeleteStudentRequest request, CancellationToken cancellationToken)
        {
            User? currentUser = await auth.GetCurrentUser();
            if(currentUser==null)
            {
                _logger.LogWarning("User Not Exists");
                return Result<StudentResponse>.Failure("User does not exists");

            }

            var isStudentExists = await unitOfRepositories.GetRepository<ZestTechnicalAssignment.Domain.Entities.Student>().GetById(request.Id);

            if (isStudentExists == null) return Result<StudentResponse>.Failure("Student Not Exites");
            isStudentExists.IsDeleted  = true;

            await unitOfRepositories.GetRepository<ZestTechnicalAssignment.Domain.Entities.Student>().Update(isStudentExists);
            await unitOfRepositories.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Student Deleted Successfully");
            return Result<StudentResponse>.Successs(mapper.Map<StudentResponse>(isStudentExists));
        }
    }
}
