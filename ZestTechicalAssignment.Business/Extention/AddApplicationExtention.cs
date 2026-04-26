using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ZestTechicalAssignment.Business.AutoMapper;
using ZestTechicalAssignment.Business.Behaviour;


namespace ZestTechicalAssignment.Business.Extention
{
    public static class AddApplicationExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(Mapping));
            services.AddMediatR(cg =>
            {
                cg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
            return services;
        }
    }
}
