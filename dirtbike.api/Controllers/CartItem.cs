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

        //[HttpGet]
        group.MapGet("/cart/user/{Userid}", (int userid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETITEMSFORCARTBYID", 1, "Test", "Test");
                return context.Cartitems.Where(m => m.Userid == userid).ToList();
            }
        })
        .WithName("GetCartitemsByUserId")
        .WithOpenApi();





        //[HttpPut]
        group.MapPut("/{id}", async (int id, Cartitem input) =>
        {
            using (var context = new DirtbikeContext())
            {
                var existingItem = context.Cartitems.FirstOrDefault(m => m.Id == id);
        		if (existingItem == null)
        		{
            	return Results.NotFound($"Cartitem with ID {id} not found.");
        		}

        		context.Cartitems.Attach(existingItem);    
            
                                    
               if (input.Cartid != null && input.Cartid != 0) existingItem.Cartid = input.Cartid;
        if (!string.IsNullOrEmpty(input.Parkname)) existingItem.Parkname = input.Parkname;
        if (input.Cartitemdate != null) existingItem.Cartitemdate = input.Cartitemdate;
        if (!string.IsNullOrEmpty(input.Itemvendor)) existingItem.Itemvendor = input.Itemvendor;
        if (!string.IsNullOrEmpty(input.Itemdescription)) existingItem.Itemdescription = input.Itemdescription;
        if (input.Itemextendedprice != null && input.Itemextendedprice != 0) existingItem.Itemextendedprice = input.Itemextendedprice;
        if (input.Itemqty != 0) existingItem.Itemqty = input.Itemqty;
        if (input.Itemtotals != 0) existingItem.Itemtotals = input.Itemtotals;
        if (input.Salescatid != null && input.Salescatid != 0) existingItem.Salescatid = input.Salescatid;
        if (!string.IsNullOrEmpty(input.Productid)) existingItem.Productid = input.Productid;
        if (input.Shopid != null) existingItem.Shopid = input.Shopid;
        if (input.Parkid != null) existingItem.Parkid = input.Parkid;
        if (input.Subtotal != null && input.Subtotal != 0) existingItem.Subtotal = input.Subtotal;
        if (input.CreatedDate != null) existingItem.CreatedDate = input.CreatedDate;
        if (input.ResStart != null) existingItem.ResStart = input.ResStart;
        if (input.ResEnd != null) existingItem.ResEnd = input.ResEnd;
        if (!string.IsNullOrEmpty(input.Qrcodeurl)) existingItem.Qrcodeurl = input.Qrcodeurl;
        if (!string.IsNullOrEmpty(input.Reservationcode)) existingItem.Reservationcode = input.Reservationcode;
        if (input.Memberid != null) existingItem.Memberid = input.Memberid;
        if (!string.IsNullOrEmpty(input.Rewardsprovider)) existingItem.Rewardsprovider = input.Rewardsprovider;
        if (input.Adults != 0) existingItem.Adults = input.Adults;
        if (input.Children != 0) existingItem.Children = input.Children;
        if (input.Statetaxpercent != 0) existingItem.Statetaxpercent = input.Statetaxpercent;
        if (!string.IsNullOrEmpty(input.Statetaxauth)) existingItem.Statetaxauth = input.Statetaxauth;
        if (input.Ustaxpercent != 0) existingItem.Ustaxpercent = input.Ustaxpercent;
        if (input.Ustaxtotal != 0) existingItem.Ustaxtotal = input.Ustaxtotal;
        if (input.Statetaxtotal != 0) existingItem.Statetaxtotal = input.Statetaxtotal;
        if (input.Itemsubtotal != 0) existingItem.Itemsubtotal = input.Itemsubtotal;
	if (input.Userid != 0) existingItem.Userid = input.Userid;


        		await context.SaveChangesAsync();
                //LOG API CALL
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

