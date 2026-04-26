using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ZestTechicalAssignment.Business.Response.StudentRes;
using ZestTechnicalAssignment.Shared.ApiResponseModel;

namespace ZestTechicalAssignment.Business.Request.StudentRequest
{
    public class DeleteStudentRequest : IRequest<Result<StudentResponse>>
    {
        public Guid Id { get; set; }
    }
}
