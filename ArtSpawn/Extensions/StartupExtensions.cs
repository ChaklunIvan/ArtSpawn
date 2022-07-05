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
using ArtSpawn.Helpers.Validators;
using FluentValidation.AspNetCore;

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

            services.AddValidatorsFromAssemblyContaining<ArtistUpdateValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryUpdateValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductUpdateValidator>();

            return services;
        }
    }
}
