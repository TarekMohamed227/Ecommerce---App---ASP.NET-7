using System.Reflection;
using AutoMapper;
using Ecom.API.Errors;
using Ecom.API.Extentions;
using Ecom.API.Middleware;
using Ecom.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.ApiRegestiration(); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InfrastructureConfiguration(builder.Configuration);


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>(); // Status code 500 Exception Handler
app.UseStatusCodePagesWithReExecute("/errors/{0}"); // Worng api request Handler
app.UseCors("CorsPolicy");  //Enable CORS

app.UseHttpsRedirection();
app.UseStaticFiles();  //Images Handler
app.UseAuthorization();

app.MapControllers();

app.Run();
