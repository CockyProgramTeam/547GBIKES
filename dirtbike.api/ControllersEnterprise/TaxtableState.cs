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


public static class TaxtableStateEndpoints
{
    
    public static void MapTaxtableStateEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/TaxtableState").WithTags(nameof(TaxtableState));
        Enterpriseservices.Globals.ControllerAPIName = "TaxtableStateAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.TaxtableStates.ToList();
            }
            
        })
        .WithName("GetAllTaxtableStates")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.TaxtableStates.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetTaxtableStateById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, TaxtableState input) =>
        {
            using (var context = new DirtbikeContext())
            {
                TaxtableState[] someTaxtableState = context.TaxtableStates.Where(m => m.Id == id).ToArray();
                context.TaxtableStates.Attach(someTaxtableState[0]);
                if (input.State != null) someTaxtableState[0].State = input.State;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateTaxtableState")
        .WithOpenApi();

        group.MapPost("/", async (TaxtableState input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.TaxtableStates.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateTaxtableState")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.TaxtableStates.Add(std);
                TaxtableState[] someTaxtableStates = context.TaxtableStates.Where(m => m.Id == id).ToArray();
                context.TaxtableStates.Attach(someTaxtableStates[0]);
                context.TaxtableStates.Remove(someTaxtableStates[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteTaxtableState")
        .WithOpenApi();
    }
}
