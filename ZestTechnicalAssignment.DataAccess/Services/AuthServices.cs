using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZestTechicalAssignment.Business.Interfaces;
using ZestTechicalAssignment.Business.Request.AuthRequest;
using ZestTechnicalAssignment.Domain.Entities;
using ZestTechnicalAssignment.Shared.ApiResponseModel;

namespace ZestTechnicalAssignment.DataAccess.Services
{
    public class AuthServices(UserManager<User> userManager ,IConfiguration configuration ,IMapper mapper,IHttpContextAccessor httpContextAccessor,IUnitOfRepositories unitOfRepositories) : IAuthRepositories
    {
        public async Task<Result<string>> Login(LoginRequest request)
        {
            User? user = await userManager.FindByEmailAsync(request.Email);

            if(user == null) return Result<string>.Failure("User Not Exists");

            if (String.IsNullOrWhiteSpace(request.Password))
            {
                return Result<string>.Failure("Password has not provided");
            }

            var checkPassword  =await userManager.CheckPasswordAsync(user, request.Password);

            if(!checkPassword)
                return Result<string>.Failure("Wrong Password");

            IList<string> roles = await userManager.GetRolesAsync(user);

            string token = CreateJwtToken(user, roles) ;

            return Result<string>.Successs(token);


        }

        public async Task<Result<string>> Registration(RegistrationRequest request)
        {
            if(request==null||String.IsNullOrWhiteSpace(request.Password)||String.IsNullOrWhiteSpace(request.Email))
                return Result<string>.Failure("Registration request are empty");

            User? newUser = mapper.Map<User>(request);
            newUser.UserName = request.Email;

            IdentityResult hassedPassword = await userManager.CreateAsync(newUser,request.Password) ;

            if(hassedPassword.Succeeded)
            {
                hassedPassword = await userManager.AddToRoleAsync(newUser, "User");

                return Result<string>.Successs(request.Name);
            }

            IEnumerable<IdentityError> errors = hassedPassword.Errors;  
            StringBuilder errorsMessages = new ();

            foreach (var er in errors)
                errorsMessages.AppendLine(er.Description);

            return Result<string>.Failure(errorsMessages.ToString());



        }



        public string CreateJwtToken(User user, IList<string> roles)
        {
            List<Claim> claims =
            [
                new(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new(ClaimTypes.Name , user.Name),
                new(ClaimTypes.Email , user.Email ?? String.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ];

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SymmetricSecurityKey? Key = new(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty));
            SigningCredentials credentials = new(Key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User?> GetCurrentUser()
        {
            string currentUserId = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            Guid id = Guid.TryParse(currentUserId, out var userId) ? userId : Guid.Empty;
            if (id == Guid.Empty)
            {
                return null;
            }
            var guidId = Guid.Parse(currentUserId);
            User? user =  await unitOfRepositories.GetRepository<User>().GetById(guidId);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
