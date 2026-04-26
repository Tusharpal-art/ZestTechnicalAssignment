using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZestTechicalAssignment.Business.Request.AuthRequest
{
    public class RegistrationRequest:IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

    }
}
