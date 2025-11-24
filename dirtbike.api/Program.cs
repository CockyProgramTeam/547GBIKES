using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.Text;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Microsoft.OpenApi.Models;
using Enterprise.Controllers;
using System.Text.Json;
using ParkTools;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

// Load CORS settings from appsettings.json
var corsSettings = builder.Configuration.GetSection("Cors");
var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
var allowedMethods = corsSettings.GetSection("AllowedMethods").Get<string[]>() ?? Array.Empty<string>();
var allowedHeaders = corsSettings.GetSection("AllowedHeaders").Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("UnifiedCors", policy =>
    {
        policy.SetIsOriginAllowed(origin =>
            string.IsNullOrEmpty(origin) || origin == "null" || allowedOrigins.Contains(origin))
            .WithMethods(allowedMethods)
            .WithHeaders(allowedHeaders);
    });
});

var app = builder.Build();
app.UseMiddleware<SwaggerAuthMiddleware>();

// Use middleware
app.UseCors("UnifiedCors");
app.UseSwagger();
app.UseSwaggerUI();
app.MapCartEndpoints();
app.MapParksEndpoints();
app.MapApilogEndpoints();
app.MapUserlogEndpoints();
app.MapCardEndpoints();
app.MapBookingEndpoints();
//app.MapTestBookingEndpoints();
app.MapUserEndpoints();
app.MapUserprofileEndpoints();
app.MapUsersessionEndpoints();
app.MapSessionlogEndpoints();
app.MapSalesCatalogueEndpoints();
app.MapPaymentEndpoints();
app.MapCustomerEndpoints();
app.MapParkReviewEndpoints();
app.MapEmployeeEndpoints();
app.MapCompanyEndpoints();
app.MapSiteEndpoints();
app.MapCartitemEndpoints();
app.MapCartMasterEndpoints();
app.MapEmailNotificationEndpoints();
app.MapAdminlogsEndpoints();
app.MapSuperuserlogEndpoints();
app.MapNoctechsEndpoints();
app.MapControllers();
app.MapParkCalendarEndpoints();
app.MapLearnlogEndpoints();
app.MapUseractionEndpoints();
app.MapUsergroupsEndpoints();
//app.MapUseractionEndpoints();
app.MapUserhelpEndpoints();
app.MapBatchEndpoints();
app.MapBatchtypeEndpoints();
app.UseMiddleware<SwaggerAuthMiddleware>();
app.Run();
