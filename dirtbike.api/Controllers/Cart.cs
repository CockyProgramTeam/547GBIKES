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


public static class CartEndpoints
{
    
    public static void MapCartEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Cart").WithTags(nameof(Cart));
        Enterpriseservices.Globals.ControllerAPIName = "CartAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Carts.ToList();
            }
            
        })
        .WithName("GetAllCarts")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Carts.Where(m => m.CartId == id).ToList();
            }
        })
        .WithName("GetCartById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Cart input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Cart[] someCart = context.Carts.Where(m => m.CartId == id).ToArray();
                context.Carts.Attach(someCart[0]);
                someCart[0].ItemDescription = input.ItemDescription;
                someCart[0].Uid = input.Uid;
                someCart[0].ParkId = input.ParkId;
                someCart[0].ItemType = input.ItemType;
                someCart[0].Quantity = input.Quantity;
                someCart[0].UnitPrice = input.UnitPrice;
                someCart[0].TotalPrice = input.TotalPrice;
                someCart[0].DateAdded = input.DateAdded;
                someCart[0].IsCheckedOut = input.IsCheckedOut;
             
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.CartId);
            }


        })
        .WithName("UpdateCart")
        .WithOpenApi();

        group.MapPost("/", async (Cart input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Carts.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.CartId);
            }

        })
        .WithName("CreateCart")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Carts.Add(std);
                Cart[] someCarts = context.Carts.Where(m => m.CartId == id).ToArray();
                context.Carts.Attach(someCarts[0]);
                context.Carts.Remove(someCarts[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteCart")
        .WithOpenApi();
    }
}

