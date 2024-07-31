using LogisticsApis.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LogisticsApis.Configuration;
using LogisticsApis.Repositories;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5174") // Replace with your Vue.js app's origin
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program)); // Assuming Startup is the class where AutoMapper is configured


// Configure JWT settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));


builder.Services.AddDbContext<LogisticsAPIDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LogisticsApiConnectionString")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowVueApp"); // Apply CORS policy

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
