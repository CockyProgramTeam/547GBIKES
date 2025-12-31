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


public static class TaxtableUEndpoints
{
    
    public static void MapTaxtableUSEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/TaxtableU").WithTags(nameof(TaxtableU));
        Enterpriseservices.Globals.ControllerAPIName = "TaxtableUAPI";
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
        .WithName("GetAllTaxtableU")
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
        .WithName("GetTaxtableUById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, TaxtableU input) =>
        {
            using (var context = new DirtbikeContext())
            {
                TaxtableU[] someTaxtableU = context.TaxtableUs.Where(m => m.Id == id).ToArray();
                context.TaxtableUs.Attach(someTaxtableU[0]);
                if (input.Uspersonallow != null) someTaxtableU[0].Uspersonallow = input.Uspersonallow;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateTaxtableU")
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
        .WithName("CreateTaxtableU")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.TaxtableU.Add(std);
                TaxtableU[] someTaxtableU = context.TaxtableUs.Where(m => m.Id == id).ToArray();
                context.TaxtableUs.Attach(someTaxtableU[0]);
                context.TaxtableUs.Remove(someTaxtableU[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteTaxtableU")
        .WithOpenApi();
    }
}

