using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using ZestTechicalAssignment.Business.Extention;
using ZestTechicalAssignment.Business.Interfaces;
using ZestTechnicalAssignment.DataAccess.ApplicationContext;
using ZestTechnicalAssignment.DataAccess.Repositories;
using ZestTechnicalAssignment.DataAccess.Services;
using ZestTechnicalAssignment.Domain.Entities;
using ZestTechnicalAssisgnMent.Api.ExceptionHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentManagement", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Bearer",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddDbContext<ApplicationDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DellConnectionString")));
builder.Services.AddScoped<IStudentRepositories, StudentRepositories>();
builder.Services.AddScoped<IAuthRepositories, AuthServices>();
builder.Services.AddScoped<IUnitOfRepositories, UnitOfRepositories>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddIdentityCore<User>()
.AddRoles<IdentityRole<Guid>>()
.AddTokenProvider<DataProtectorTokenProvider<User>>("StudentManagement")
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? String.Empty))
            });

builder.Services.Configure<IdentityOptions>(o =>
{
    o.Password.RequireDigit = true;
    o.Password.RequireLowercase = true;
    o.Password.RequiredLength = 6;
    o.Password.RequiredUniqueChars = 1;
    o.Password.RequireUppercase = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddApplicationServices();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapOpenApi();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseExceptionHandler(_ => { });
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
