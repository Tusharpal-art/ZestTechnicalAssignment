using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ZestTechicalAssignment.Business.Interfaces;

namespace ZestTechnicalAssisgnMent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepositories authRepositories,ILogger<AuthController> _logger) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(ZestTechicalAssignment.Business.Request.AuthRequest.LoginRequest request)
        {
            var resposne = await authRepositories.Login(request);
            _logger.LogInformation("Login Successfully");
            return Ok(resposne);

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(ZestTechicalAssignment.Business.Request.AuthRequest.RegistrationRequest request)
        {
            var resposne = await authRepositories.Registration(request);
            _logger.LogInformation("Register Successfully");
            return Ok(resposne);

        }
    }
}
