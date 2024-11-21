using System.Text;
using Application.EF;
using Application.Uow;
using AutoMapper;
using Business.Services;
using Dtos.Configure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Utilities;

namespace Business.Extensions;

public static class BuildServiceExtensions
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var mapperConfiguration = new MapperConfiguration(
            config => { config.AddProfile<MapProfile>(); });

        IMapper mapper = mapperConfiguration.CreateMapper();
        services.AddSingleton(mapper);
        return services;
    }

    public static IServiceCollection AddCaching(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddConfigureOptions(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Name));
        services.AddScoped<JwtManager>();
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                JwtOptions jwt = configuration.GetSection(JwtOptions.Name).Get<JwtOptions>();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(CryptoUtils.Decode(jwt.Key)))
                };
            });
        services.AddAuthorization();
        return services;
    }


    
    
    public static IServiceCollection AddBlogDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<BlogDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<TagService>();
        return services;
    }
}