using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZestTechicalAssignment.Business.Interfaces;
using ZestTechicalAssignment.Business.Request.StudentRequest;

namespace ZestTechnicalAssisgnMent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IMediator mediator) : ControllerBase
    {
        [HttpPost("AddStudent")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentRequest request)
        {
            var resposne = await mediator.Send(request);
            return Ok(resposne);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateRequest([FromBody] UpdateStudentRequest request)
        {
            var resposne = await  mediator.Send(request);
            return Ok(resposne);

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent([FromBody] DeleteStudentRequest request)
        {
            var resposne = await mediator.Send(request);
            return Ok(resposne);

        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("GetAllStudent")]
        
        public async Task<IActionResult> GetAllStudent([FromQuery] GetAllStudentRequest request)
        {
            var resposne = await mediator.Send(request);
            return Ok(resposne);

        }
    }
}
