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
    
        group.MapGet("/user/{uid}", (string uid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Carts.Where(m => m.Uid == uid).ToList();
            }
        })
        .WithName("GetCartByUser")
        .WithOpenApi();
        

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Cart input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Cart[] someCart = context.Carts.Where(m => m.CartId == id).ToArray();
                context.Carts.Attach(someCart[0]);
			if (input.ItemDescription != null) someCart[0].ItemDescription = input.ItemDescription;
			if (input.Uid != null) someCart[0].Uid = input.Uid;
			//someCart[0].ParkId = input.ParkId; DECIDED TO NOT ALLOW THIS
			if (input.ItemType != null) someCart[0].ItemType = input.ItemType;
			someCart[0].Quantity = input.Quantity;
			someCart[0].UnitPrice = input.UnitPrice;
			 someCart[0].TotalPrice = input.TotalPrice;
			 if (input.DateAdded != null) someCart[0].DateAdded = input.DateAdded;
			 if (input.ResStart != null) someCart[0].ResStart = input.ResStart;
			 if (input.ResEnd != null) someCart[0].ResEnd = input.ResEnd;
			 if (input.IsCheckedOut != null) someCart[0].IsCheckedOut = input.IsCheckedOut;
			 someCart[0].Totalcartitems = input.Totalcartitems;
			 if (input.Paymentid != null) someCart[0].Paymentid = input.Paymentid;
			 if (input.Bookinginfo != null) someCart[0].Bookinginfo = input.Bookinginfo;
                 	 someCart[0].Johnstotals = input.Johnstotals;
    			 someCart[0].Transactiontotal = input.Transactiontotal;
			 someCart[0].Adults = input.Adults;
			 someCart[0].Children = input.Children;
			 someCart[0].Tentsites = input.Tentsites;
    			 //if(input.Parkname !=null) someCart[0].Parkname = input.Parkname; DECIDED TO EXPLICITLY DENY THIS FUNCTION
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "UPDATEWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.CartId);
            }
        })
        .WithName("UpdateCart")
        .WithOpenApi();
    
  
            //[HttpPut]
        group.MapPut("/mark/{cartId}", async (int cartId, int bookingId) =>    
        {
            using (var context = new DirtbikeContext())
            {
                Cart[] someCart = context.Carts.Where(m => m.CartId == cartId).ToArray();
                context.Carts.Attach(someCart[0]);
				someCart[0].IsCheckedOut = 2;
	            await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated CartID: " + cartId);
            }
        })
        .WithName("UpdateCartWithTempItems")
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
    
    
    group.MapPost("/newparent/", async (int cartId, int parkId, string bookingInfo) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
            	Cart input = new Cart();
                context.Carts.Add(input);
                input.CartId = cartId;
                input.ParkId = parkId;
                input.Bookinginfo = bookingInfo;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORDWITHPARAMS", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.CartId);
            }

        })
        .WithName("CreateCartTempItemFromVariables")
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

