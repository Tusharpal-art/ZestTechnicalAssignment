using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using ZestTechicalAssignment.Business.Request.AuthRequest;
using ZestTechicalAssignment.Business.Request.StudentRequest;
using ZestTechicalAssignment.Business.Response.StudentRes;
using ZestTechnicalAssignment.Domain.Entities;

namespace ZestTechicalAssignment.Business.AutoMapper
{
    public class Mapping:Profile
    {
        public Mapping() 
        {
            CreateMap<AddStudentRequest,Student>().ReverseMap();
            CreateMap<StudentResponse,Student>().ReverseMap();
            CreateMap<RegistrationRequest,User>().ReverseMap();

        }
    }
}
