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


public static class ParkCalendarDayEndpoints
{
    
    public static void MapParkCalendarDayEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/ParkCalendarDay").WithTags(nameof(ParkCalendarDay));
        Enterpriseservices.Globals.ControllerAPIName = "ParkCalendarDayAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.ParkCalendarDays.ToList();
            }
            
        })
        .WithName("GetAllParkCalendarDays")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.ParkCalendarDays.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetParkCalendarDayById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, ParkCalendarDay input) =>
        {
            using (var context = new DirtbikeContext())
            {
                ParkCalendarDay[] someParkCalendarDay = context.ParkCalendarDays.Where(m => m.Id == id).ToArray();
                context.ParkCalendarDays.Attach(someParkCalendarDay[0]);
                if (input.Parkguid != null) someParkCalendarDay[0].Parkguid = input.Parkguid;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateParkCalendarDay")
        .WithOpenApi();

        group.MapPost("/", async (ParkCalendarDay input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.ParkCalendarDays.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateParkCalendarDay")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.ParkCalendarDays.Add(std);
                ParkCalendarDay[] someParkCalendarDays = context.ParkCalendarDays.Where(m => m.Id == id).ToArray();
                context.ParkCalendarDays.Attach(someParkCalendarDays[0]);
                context.ParkCalendarDays.Remove(someParkCalendarDays[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteParkCalendarDay")
        .WithOpenApi();
    }
}

