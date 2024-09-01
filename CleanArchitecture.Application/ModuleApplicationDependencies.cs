using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Services.AuthenticationService;
using CleanArchitecture.Application.Services.EmailsService;
using CleanArchitecture.Application.Services.FileService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Application
{
    public static class ModuleApplicationDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            services.AddTransient<IEmailsService, EmailsService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            //Configuration Of Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //Configuration Of Automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }

    }
}
