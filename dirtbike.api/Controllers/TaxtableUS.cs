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


public static class TaxtableUSEndpoints
{
    
    public static void MapTaxtableUSEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/TaxtableUS").WithTags(nameof(TaxtableU));
        Enterpriseservices.Globals.ControllerAPIName = "TaxtableUSAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.TaxtableUs.ToList();
            }
            
        })
        .WithName("GetAllTaxtableUSs")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.TaxtableUs.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetTaxtableUSById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, TaxtableU input) =>
        {
            using (var context = new DirtbikeContext())
            {
                TaxtableU[] someTaxtableUS = context.TaxtableUs.Where(m => m.Id == id).ToArray();
                context.TaxtableUs.Attach(someTaxtableUS[0]);
                if (input.Uspersonallow != null) someTaxtableUS[0].Uspersonallow = input.Uspersonallow;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateTaxtableUS")
        .WithOpenApi();

        group.MapPost("/", async (TaxtableU input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.TaxtableUs.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateTaxtableUS")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.TaxtableUSs.Add(std);
                TaxtableU[] someTaxtableUSs = context.TaxtableUs.Where(m => m.Id == id).ToArray();
                context.TaxtableUs.Attach(someTaxtableUSs[0]);
                context.TaxtableUs.Remove(someTaxtableUSs[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteTaxtableUS")
        .WithOpenApi();
    }
}

