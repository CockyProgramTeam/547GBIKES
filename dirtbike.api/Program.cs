using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure.Messaging.ServiceBus;
using Enterprise.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ParkTools;
using Services;
using System.Text;
using System.Text.Json;
using dirtbike.api.Services;
using dirtbike.api.Models;
using dirtbike.api.Data;

namespace Enterpriseservices
{
class Program
{
    static async Task Main(string[] args)
    {
    var builder = WebApplication.CreateBuilder(args);

        // Add services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ParksAPI", Version = "42" });
        });

//AZURE SERVICES FROM 590 WITH DEPENDENCY INJECTION (COLIN SERVICE BUS, SAMBIT OCR)

builder.Services.AddSingleton(sp =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();

    Console.WriteLine("Using OCR Endpoint: " + cfg["ComputerVision:Endpoint"]);
    Console.WriteLine("Using OCR Key: " + cfg["ComputerVision:Key"]?.Substring(0, 4) + "...");

    return new DocumentAnalysisClient(
        new Uri(cfg["ComputerVision:Endpoint"]),
        new AzureKeyCredential(cfg["ComputerVision:Key"])
    );
});

// Register Service Bus Service
builder.Services.AddSingleton<ServiceBusService>();


//We Decided to start with an Elaborate CORS Policy. This is Ignored on Azure Cloud as they overwrite it.... but on Hosted Glcloud its required.
// Load CORS settings from appsettings.json
var corsSettings = builder.Configuration.GetSection("Cors");
var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
var allowedMethods = corsSettings.GetSection("AllowedMethods").Get<string[]>() ?? Array.Empty<string>();
var allowedHeaders = corsSettings.GetSection("AllowedHeaders").Get<string[]>() ?? Array.Empty<string>();


// JWT TOKEN IS USED IN AUTH CONTROLLER TO GENERATE 128 BIT KEY.

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
app.MapCreditCardEndpoints();
app.MapAllCGUserEndpoints();
app.MapUserNoticeEndpoints();
app.MapParkCalendarDayEndpoints();
app.MapUserDTOEndpoints();
//app.Run();
//THIS ROUTINE RUNS A PASSWORD HASHER AGAINST THE CURRENT USER TABLE.
//IT WILL REBUILD THE PASSWORDS ALSO USING A RANDOM HASHER USING BCRYPT
//THE SAME PASSWORD WILL GENERATE A UNIQUE STRING EVERY TIME.
//AUTH WILL FAIL WITHOUT THE BCRYPT SO EVEN HAVING THE PLAIN PASSWORD IS NO HELP.
//COMMENTED OUT AS IT SHOULD ONLY BE RUN WITH ADMINISTRATOR PERMISSION.
//WE DO NEED TO CONSIDER WHETHER THE /API/USER GET NEEDS TO BE PRESENT AND OR PERMISSIONS ON USERMANAGER.

//var myPasswords = new MyPasswords();
//await MyPasswords.HashAllUserPasswordsAsync();

if (builder.Environment.IsDevelopment()) { await RunCliAsync(); } 
await app.RunAsync();
}

    static async Task RunCliAsync()
{
    Console.WriteLine("=== Dirtbike System Console CLI ===");
    Console.WriteLine("Type 'help' for commands, 'exit' to quit.");

    string? input;
    do
    {
        Console.Write("> ");
        input = Console.ReadLine()?.Trim().ToLower();

        switch (input)
        {
            case "help":
                Console.WriteLine("Available commands:");
                Console.WriteLine("  schema     - Dump DB schema");
                Console.WriteLine("  ncparks    - Process NC parks from DATA directory");
                Console.WriteLine("  vaparks    - Process VA parks from DATA directory");
                Console.WriteLine("  allparks   - Process ALL parks in IOQUEUE via SQL");
                Console.WriteLine("  files      - Show file list");
                Console.WriteLine("  zerocarts  - Remove zero carts for a user");
                Console.WriteLine("  avg        - Update avg rating for one park");
                Console.WriteLine("  avgall     - Update avg rating for first 500 parks");
                Console.WriteLine(" initdata - Load initial.sql (parks, users, reviews)");
                Console.WriteLine("  exit       - Quit CLI");
                break;

            case "schema":
                Enterpriseservices.SystemCLISupport.DumpSchema();
                break;

            case "ncparks":
                SystemCLISupport.ProcessNCParks();
                break;

            case "vaparks":
                SystemCLISupport.ProcessVAParks();
                break;

            case "allparks":
                SystemCLISupport.ProcessAllParks();
                break;

            case "files":
                SystemCLISupport.ShowFileList();
                break;

            case "initdata": 
                Console.WriteLine("Running initial.sql..."); 
                Enterpriseservices.DatabaseTools.LoadInitData(); 
                break;

            case "zerocarts":
                Console.Write("Enter user ID: ");
                if (int.TryParse(Console.ReadLine(), out int userId))
                    SystemCLISupport.RemoveZeroCarts(userId);
                else
                    Console.WriteLine("Invalid user ID.");
                break;

            case "avg":
                Console.Write("Enter park ID: ");
                if (int.TryParse(Console.ReadLine(), out int parkId))
                    SystemCLISupport.UpdateParkAvg(parkId);
                else
                    Console.WriteLine("Invalid park ID.");
                break;

            case "avgall":
                SystemCLISupport.UpdateAllParkAvgs();
                break;

            case "exit":
                Console.WriteLine("Exiting CLI...");
                break;

            default:
                if (!string.IsNullOrWhiteSpace(input))
                    Console.WriteLine($"Unknown command: {input}");
                break;
        }

    } while (input != "exit");

    await Task.CompletedTask;
}
}}

