using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using Ecom.API.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Ecom.API.Extentions
{
    public static class ApiRegestration
    {
        public static IServiceCollection ApiRegestiration(this IServiceCollection service)
        {

            //Configure AutoMapper
            service.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Configure IFileProvider => Image Handler

            service.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));


            service.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray()
                    };
                    return new BadRequestObjectResult(errorResponse);
                };

            });

            //Enable CORS
            service.AddCors(OPT =>
            {
                OPT.AddPolicy("CorsPolicy", pol => {

                    pol.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });
            return service;
        }
    }
}
