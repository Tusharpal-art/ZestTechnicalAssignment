using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ZestTechicalAssignment.Business.Response.StudentRes;
using ZestTechnicalAssignment.Shared.ApiResponseModel;

namespace ZestTechicalAssignment.Business.Request.StudentRequest
{
    public class UpdateStudentRequest : IRequest<Result<StudentResponse>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }
        public string? Course { get; set; }
    }
}
