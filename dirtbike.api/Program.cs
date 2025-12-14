using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure.Messaging.ServiceBus;
using Enterprise.Controllers;
using Enterpriseservices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ParkTools;
using Services;
using System.Text;
using System.Text.Json;
using dirtbike.api.Services;

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
//REMOVED DI FOR THIS SERVICE builder.Services.AddScoped<CGCartService>();
//REMOVED DI FOR THIS SERVICE builder.Services.AddScoped<CGPARKSService>();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ServiceBus:ConnectionString"];
var queueName = builder.Configuration["ServiceBus:QueueName"];

builder.Services.AddSingleton(new ServiceBusClient(connectionString));
builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<ServiceBusClient>();
    return client.CreateSender(queueName);
});

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
app.MapRefundEndpoints();
app.MapTaxtableStateEndpoints();
app.MapTaxtableUSEndpoints();
app.MapCGUIParksEndpoints();
app.MapCGUICartEndpoints();
app.MapAuthEndpoints();
app.MapUserPictureEndpoints();
app.MapParkInventoryEndpoints();

//THIS ROUTINE RUNS A PASSWORD HASHER AGAINST THE CURRENT USER TABLE.
//IT WILL REBUILD THE PASSWORDS ALSO USING A RANDOM HASHER USING BCRYPT
//THE SAME PASSWORD WILL GENERATE A UNIQUE STRING EVERY TIME.
//AUTH WILL FAIL WITHOUT THE BCRYPT SO EVEN HAVING THE PLAIN PASSWORD IS NO HELP.
//COMMENTED OUT AS IT SHOULD ONLY BE RUN WITH ADMINISTRATOR PERMISSION.
//WE DO NEED TO CONSIDER WHETHER THE /API/USER GET NEEDS TO BE PRESENT AND OR PERMISSIONS ON USERMANAGER.

//var myPasswords = new MyPasswords();
//await MyPasswords.HashAllUserPasswordsAsync();

app.Run();


