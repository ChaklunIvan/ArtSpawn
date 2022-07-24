using ArtSpawn.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ArtSpawn.Models.Exceptions;
using FluentValidation;
using FluentValidation.AspNetCore;
using ArtSpawn.Models.Entities;
using Microsoft.AspNetCore.Identity;
using ArtSpawn.Configurations.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ArtSpawn.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .WithMethods("POST", "GET", "PUT", "DELETE")
                .AllowAnyHeader());
            });
            return services;
        }

        public static IServiceCollection ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }

        public static IServiceCollection AddProblemDetails(this IServiceCollection services, IHostEnvironment env)
        {
            services.AddProblemDetails(config =>
            {
                config.IncludeExceptionDetails = (_, _) => env.IsDevelopment();

                config.Map<NotFoundException>(exception => new ProblemDetails
                {
                    Type = exception.GetType().ToString(),
                    Detail = exception.Message,
                    Status = (int)HttpStatusCode.NotFound,
                });

                config.Map<BadRequestException>(exception => new ProblemDetails
                {
                    Type = exception.GetType().ToString(),
                    Detail = exception.Message,
                    Status = (int)HttpStatusCode.BadRequest,
                });

                config.Map<UnauthorizedException>(exception =>
                new ProblemDetails
                {
                    Type = exception.GetType().ToString(),
                    Detail = exception.Message,
                    Status = (int)HttpStatusCode.Unauthorized,
                });

                config.Map<Exception>(exception => new ProblemDetails
                {
                    Type = exception.GetType().ToString(),
                    Detail = exception.Message,
                    Status = (int)HttpStatusCode.InternalServerError,
                });

                
            });

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<ArtistRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UserRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<AuthenticationRequestValidator>();

            services.AddValidatorsFromAssemblyContaining<ArtistUpdateValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryUpdateValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductUpdateValidator>();

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 8;
                o.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                        ValidAudience = jwtSettings.GetSection("validAudience").Value,
                        IssuerSigningKey = new
                        SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))

                    };
                });
            return services;
        }
    }
}
