using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using dirtbike.api.Models;
using dirtbike.api.Data;
using Enterpriseservices;
using Microsoft.Extensions.WebEncoders.Testing;
using dirtbike.api.DTOs;

namespace Enterprise.Controllers;

public static class UserDTOEndpoints
{
    public static void MapUserDTOEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/QuickUser").WithTags(nameof(Template));
        Enterpriseservices.Globals.ControllerAPIName = "QuickUserAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";

        // NEW SERVICE-BASED IMPLEMENTATION
        group.MapPost("/quickadd", async ([FromBody] QuickUserAdd dto) =>
        {
            Console.WriteLine($"QuickUserAdd DTO received: Username={dto.Username}, Fullname={dto.Fullname}, Email={dto.Email}, Role={dto.Role}");

            string logPath = "/opt/ga/547bikes/logs/quickusers.log";
            string logEntry = $"[{DateTime.Now}] QuickUserAdd received: Username={dto.Username}, Fullname={dto.Fullname}, Email={dto.Email}, Role={dto.Role}{Environment.NewLine}";

            try
            {
                string? directoryPath = Path.GetDirectoryName(logPath);
                if (!string.IsNullOrWhiteSpace(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                File.AppendAllText(logPath, logEntry);
            }
            catch (Exception ex)
            {
                File.AppendAllText("/opt/ga/547bikes/logs/error.log", $"[{DateTime.Now}] Error: {ex.Message}{Environment.NewLine}");
            }

            // Call the service (NO DI)
            var service = new QuickUserAddService();
            var success = await service.CreateQuickUserAsync(dto);

            if (!success)
            {
                return Results.Problem("Failed to create user and related records.");
            }

            Enterpriseservices.ApiLogger.logapi(
                Enterpriseservices.Globals.ControllerAPIName,
                Enterpriseservices.Globals.ControllerAPINumber,
                "QUICKADD", 1, "QuickUserAdd", "Created");

            return TypedResults.Created($"Created User Successfully");
        })
        .WithName("QuickAddUser")
        .WithOpenApi();
    }
}
