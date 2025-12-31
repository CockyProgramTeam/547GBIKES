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
namespace Enterprise.Controllers;


public static class AdminlogsEndpoints
{
    
    public static void MapAdminlogsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Adminlogs").WithTags(nameof(Adminlog));
        Enterpriseservices.Globals.ControllerAPIName = "AdminlogsAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Adminlogs.ToList();
            }
            
        })
        .WithName("GetAllAdminlogs")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Adminlogs.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetAdminlogsById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Adminlog input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Adminlog[] someAdminlogs = context.Adminlogs.Where(m => m.Id == id).ToArray();
                context.Adminlogs.Attach(someAdminlogs[0]);
                if (input.Description != null) someAdminlogs[0].Description = input.Description;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateAdminlogs")
        .WithOpenApi();

        group.MapPost("/", async (Adminlog input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Adminlogs.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateAdminlogs")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Adminlogss.Add(std);
                Adminlog[] someAdminlogs = context.Adminlogs.Where(m => m.Id == id).ToArray();
                context.Adminlogs.Attach(someAdminlogs[0]);
                context.Adminlogs.Remove(someAdminlogs[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteAdminlogs")
        .WithOpenApi();
    }
}

