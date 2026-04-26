using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using ZestTechicalAssignment.Business.Request.StudentRequest;

namespace ZestTechicalAssignment.Business.Validation.StudentValidation
{
    public class AddStudentValidation: AbstractValidator<AddStudentRequest>
    {
        public AddStudentValidation() { 
                RuleFor(item=>item.Name).NotEmpty().MinimumLength(5);
                RuleFor(item => item.Age).NotEmpty().GreaterThan(0);
                RuleFor(item => item.Email).NotEmpty();
                RuleFor(item => item.Course).NotEmpty();


        }
    }
}
