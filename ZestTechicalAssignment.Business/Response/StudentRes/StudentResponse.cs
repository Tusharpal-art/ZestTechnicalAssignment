using System;
using System.Collections.Generic;
using System.Text;

namespace ZestTechicalAssignment.Business.Response.StudentRes
{
    public record StudentResponse(Guid Id, string Name, string Email, int Age, string Course, Guid CreatedById);
  
}
