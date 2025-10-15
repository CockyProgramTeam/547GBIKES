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


public static class SalesCatalogueEndpoints
{
    
    public static void MapSalesCatalogueEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/SalesCatalogue").WithTags(nameof(SalesCatalogue));
        Enterpriseservices.Globals.ControllerAPIName = "SalesCatalogueAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.SalesCatalogues.ToList();
            }
            
        })
        .WithName("GetAllSalesCatalogues")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.SalesCatalogues.Where(m => m.SalesCatalogueId == id).ToList();
            }
        })
        .WithName("GetSalesCatalogueById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, SalesCatalogue input) =>
        {
            using (var context = new DirtbikeContext())
            {
                SalesCatalogue[] someSalesCatalogue = context.SalesCatalogues.Where(m => m.SalesCatalogueId == id).ToArray();
                context.SalesCatalogues.Attach(someSalesCatalogue[0]);
                if (input.Description != null) someSalesCatalogue[0].Description = input.Description;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.SalesCatalogueId);
            }


        })
        .WithName("UpdateSalesCatalogue")
        .WithOpenApi();

        group.MapPost("/", async (SalesCatalogue input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.SalesCatalogues.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.SalesCatalogueId);
            }

        })
        .WithName("CreateSalesCatalogue")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.SalesCatalogues.Add(std);
                SalesCatalogue[] someSalesCatalogues = context.SalesCatalogues.Where(m => m.SalesCatalogueId == id).ToArray();
                context.SalesCatalogues.Attach(someSalesCatalogues[0]);
                context.SalesCatalogues.Remove(someSalesCatalogues[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteSalesCatalogue")
        .WithOpenApi();
    }
}

