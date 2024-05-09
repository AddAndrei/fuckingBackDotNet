using Application.Common.Behaviors;
using Application.Common.Services;
using Application.Interfaces;
using Application.Users.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<IS3CloudService, S3CloudService>();
        services.AddScoped<ICryptoService, CryptoService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
