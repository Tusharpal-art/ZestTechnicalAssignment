using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZestTechicalAssignment.Business.Request.AuthRequest
{
    public class LoginRequest:IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
