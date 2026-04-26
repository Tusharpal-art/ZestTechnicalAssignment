using System;
using System.Collections.Generic;
using System.Text;
using ZestTechicalAssignment.Business.Interfaces;
using ZestTechicalAssignment.Business.Request.StudentRequest;
using ZestTechicalAssignment.Business.Response.StudentRes;
using ZestTechnicalAssignment.DataAccess.ApplicationContext;
using ZestTechnicalAssignment.Domain.Entities;
using ZestTechnicalAssignment.Shared.ApiResponseModel;

namespace ZestTechnicalAssignment.DataAccess.Repositories
{
    public class StudentRepositories(ApplicationDBContext dbContext) : IStudentRepositories
    {
        public async Task<(int, List<Student>)> GetAllStudentListAsync(GetAllStudentRequest request)
        {
            IEnumerable<Student> students = dbContext.Students.AsEnumerable().OrderBy(item=>item.CreatedDate);

            if(request.IsDeleted)
            {
                students = students.Where(item=>item.IsDeleted);
            }
            else
            {
                students = students.Where(item => !item.IsDeleted);
            }

            var search = request.Search;
            if(search != null)
            students = students.Where(item=>item.Name.Contains(search)
            || item.Email.Contains(search) || item.Course.Contains(search));



            if(request.SortBy != null)
            {
                string sortBy = request.SortBy;
                if(sortBy.Equals("Name",StringComparison.OrdinalIgnoreCase)) 
                    students  = request.IsAscending ?  students.OrderBy(item=>item.Name) : students.OrderByDescending(item=>item.Name);
                else if (sortBy.Equals("Email", StringComparison.OrdinalIgnoreCase))
                    students = request.IsAscending ? students.OrderBy(item => item.Email) : students.OrderByDescending(item => item.Email);
                else if (sortBy.Equals("Course", StringComparison.OrdinalIgnoreCase))
                    students = request.IsAscending ? students.OrderBy(item => item.Course) : students.OrderByDescending(item => item.Course);
                else if (sortBy.Equals("Age", StringComparison.OrdinalIgnoreCase))
                    students = request.IsAscending ? students.OrderBy(item => item.Age) : students.OrderByDescending(item => item.Age);

            }

            int TotalCount = students.Count();

            List<Student> result= students.Skip(request.PageSize*(request.PageNumber - 1)).Take(request.PageSize).ToList();

            return await Task.FromResult((TotalCount,result));

        }
    }
}
