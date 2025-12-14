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


public static class ParkCalendarEndpoints
{
    
    public static void MapParkCalendarEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/ParkCalendar").WithTags(nameof(ParkCalendar));
        Enterpriseservices.Globals.ControllerAPIName = "ParkCalendarAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.ParkCalendars.ToList();
            }
            
        })
        .WithName("GetAllParkCalendars")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.ParkCalendars.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetParkCalendarById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, ParkCalendar input) =>
        {
            using (var context = new DirtbikeContext())
            {
                ParkCalendar[] someParkCalendar = context.ParkCalendars.Where(m => m.Id == id).ToArray();
                context.ParkCalendars.Attach(someParkCalendar[0]);
                someParkCalendar[0].CustomerId = input.CustomerId;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateParkCalendar")
        .WithOpenApi();

        group.MapPost("/", async (ParkCalendar input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.ParkCalendars.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateParkCalendar")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.ParkCalendars.Add(std);
                ParkCalendar[] someParkCalendars = context.ParkCalendars.Where(m => m.Id == id).ToArray();
                context.ParkCalendars.Attach(someParkCalendars[0]);
                context.ParkCalendars.Remove(someParkCalendars[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteParkCalendar")
        .WithOpenApi();
    }
}

