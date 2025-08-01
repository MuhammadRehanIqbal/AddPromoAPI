﻿using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuration setup
var configuration = builder.Configuration; // Access the app configuration

// Add services to the container
builder.Services.AddCors(options =>
{
    //options.AddPolicy("AllowAngularClient",
    //    policy => policy
    //        .WithOrigins("http://localhost:4200", "http://localhost:60729",
    //        "https://super-starlight-7d6b2e.netlify.app",
    //        "https://1b11e1e90d25.ngrok-free.app"
    //        ) // frontend origin
    //        .AllowAnyHeader()
    //        .AllowAnyMethod()
    //        .AllowCredentials()
    //    );
    options.AddPolicy("AllowNetlify", builder =>
    {
        builder
            .WithOrigins("https://chimerical-sable-936f0f.netlify.app", "http://localhost:4200") // 👈 exact Netlify domain
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // if using cookies or login headers
    });
});

// Application & Infrastructure layer dependencies
builder.Services
    .AddApplication() // Register application layer services
    .AddInfrastructure(configuration); // Register infrastructure layer services, passing configuration

// Configure JWT authentication properly with default scheme
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = configuration["JwtSettings:Issuer"],          // from appsettings.json
        ValidAudience = configuration["JwtSettings:Audience"],      // from appsettings.json
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["JwtSettings:SecurityKey"])) // your secret key from config
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Set up Swagger/OpenAPI documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();  // Enable Swagger UI in Development
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdProm API V1");
    c.RoutePrefix = "swagger"; // ensures it's served at /swagger/
});
app.UseHttpsRedirection();

//app.UseCors("AllowAngularClient");
app.UseCors("AllowNetlify");

app.UseAuthentication();  // Must come before UseAuthorization
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});
app.Run();
