using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ZestTechicalAssignment.Business.Interfaces;

namespace ZestTechnicalAssisgnMent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepositories authRepositories) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(ZestTechicalAssignment.Business.Request.AuthRequest.LoginRequest request)
        {
            var resposne = await authRepositories.Login(request);
            return Ok(resposne);

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(ZestTechicalAssignment.Business.Request.AuthRequest.RegistrationRequest request)
        {
            var resposne = await authRepositories.Registration(request);
            return Ok(resposne);

        }
    }
}
