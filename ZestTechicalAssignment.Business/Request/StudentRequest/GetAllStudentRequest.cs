using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ZestTechicalAssignment.Business.Response.StudentRes;
using ZestTechnicalAssignment.Shared.ApiResponseModel;

namespace ZestTechicalAssignment.Business.Request.StudentRequest
{
    public class GetAllStudentRequest:IRequest<Result<Tuple<int,List<StudentResponse>>>>
    {
        public bool IsDeleted { get; set; } = false;
        public string? Search { get; set; } = null;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? SortBy { get; set; } = null;
        public bool IsAscending { get; set; } = true;
    }
}
