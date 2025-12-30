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


public static class CartMasterEndpoints
{
    
    public static void MapCartMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/CartMaster").WithTags(nameof(CartMaster));
        Enterpriseservices.Globals.ControllerAPIName = "CartMasterAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.CartMasters.ToList();
            }
            
        })
        .WithName("GetAllCartMasters")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.CartMasters.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetCartMasterById")
        .WithOpenApi();
    
    
     //[HttpGet]
        group.MapGet("/user/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.CartMasters.Where(m => m.UserId == id).ToList();
            }
        })
        .WithName("GetCartMasterByUserId")
        .WithOpenApi();
    
    
       
    

        //[HttpPut]
        group.MapPut("/{id}", async (int id, CartMaster input) =>
        {
            using (var context = new DirtbikeContext())
            {
                CartMaster[] someCartMaster = context.CartMasters.Where(m => m.Id == id).ToArray();
                context.CartMasters.Attach(someCartMaster[0]);
              // Integers (nullable int? checks)
				if (input.UserId > 0) someCartMaster[0].UserId = input.UserId;
				if (input.CartsCount != null) someCartMaster[0].CartsCount = input.CartsCount;
				if (input.CartsCancelled != null) someCartMaster[0].CartsCancelled = input.CartsCancelled;
				if (input.CartsActive != null) someCartMaster[0].CartsActive = input.CartsActive;

				// Strings (check not null or empty)
				if (!string.IsNullOrEmpty(input.CartsActiveList)) someCartMaster[0].CartsActiveList = input.CartsActiveList;
				if (!string.IsNullOrEmpty(input.Loyaltyid)) someCartMaster[0].Loyaltyid = input.Loyaltyid;
				if (!string.IsNullOrEmpty(input.Loyaltyvendor)) someCartMaster[0].Loyaltyvendor = input.Loyaltyvendor;

                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateCartMaster")
        .WithOpenApi();

        group.MapPost("/", async (CartMaster input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.CartMasters.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateCartMaster")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.CartMasters.Add(std);
                CartMaster[] someCartMasters = context.CartMasters.Where(m => m.Id == id).ToArray();
                context.CartMasters.Attach(someCartMasters[0]);
                context.CartMasters.Remove(someCartMasters[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteCartMaster")
        .WithOpenApi();
    }
}

