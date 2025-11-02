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


public static class CartitemEndpoints
{
    
    public static void MapCartitemEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Cartitem").WithTags(nameof(Cartitem));
        Enterpriseservices.Globals.ControllerAPIName = "CartitemAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Cartitems.ToList();
            }
            
        })
        .WithName("GetAllCartitems")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Cartitems.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetCartitemById")
        .WithOpenApi();
    
    
           //[HttpGet]
        group.MapGet("/cart/{cartid}", (int cartid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETITEMSFORCARTBYID", 1, "Test", "Test"); 
                return context.Cartitems.Where(m => m.Cartid == cartid).ToList();
            }
        })
        .WithName("GetCartitemsByCartId")
        .WithOpenApi();
    
        
    
    
    

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Cartitem input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Cartitem[] someCartitem = context.Cartitems.Where(m => m.Id == id).ToArray();
                context.Cartitems.Attach(someCartitem[0]);
               if (input.Cartid != null) someCartitem[0].Cartid = input.Cartid;
               if (input.Parkname != null) someCartitem[0].Parkname = input.Parkname;
				if (input.Cartitemdate != null) someCartitem[0].Cartitemdate = input.Cartitemdate;
				if (input.Itemvendor != null) someCartitem[0].Itemvendor = input.Itemvendor;
				if (input.Itemdescription != null) someCartitem[0].Itemdescription = input.Itemdescription;
				if (input.Itemextendedprice != null) someCartitem[0].Itemextendedprice = input.Itemextendedprice;
				if (input.Itemqty != null) someCartitem[0].Itemqty = input.Itemqty;
				if (input.Itemtotals != null) someCartitem[0].Itemtotals = input.Itemtotals;
				if (input.Salescatid != null) someCartitem[0].Salescatid = input.Salescatid;
				if (input.Productid != null) someCartitem[0].Productid = input.Productid;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateCartitem")
        .WithOpenApi();

        group.MapPost("/", async (Cartitem input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Cartitems.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateCartitem")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Cartitems.Add(std);
                Cartitem[] someCartitems = context.Cartitems.Where(m => m.Id == id).ToArray();
                context.Cartitems.Attach(someCartitems[0]);
                context.Cartitems.Remove(someCartitems[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteCartitem")
        .WithOpenApi();
    }
}

