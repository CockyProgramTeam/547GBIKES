using Enterprise.Controllers;
using System.Text.Json;
using ParkTools;

var builder = WebApplication.CreateBuilder(args);

// Load CORS settings from appsettings.json
var corsSettings = builder.Configuration.GetSection("Cors");
var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
var allowedMethods = corsSettings.GetSection("AllowedMethods").Get<string[]>() ?? Array.Empty<string>();
var allowedHeaders = corsSettings.GetSection("AllowedHeaders").Get<string[]>() ?? Array.Empty<string>();


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
app.MapLearnlogEndpoints();
app.MapUseractionEndpoints();
app.MapUsergroupsEndpoints();
//app.MapUseractionEndpoints();
app.MapUserhelpEndpoints();
app.MapBatchEndpoints();
app.MapBatchtypeEndpoints();
app.Run();
