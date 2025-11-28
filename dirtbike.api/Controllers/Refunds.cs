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


public static class RefundEndpoints
{
    
    public static void MapRefundEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Refunds").WithTags(nameof(Refund));
        Enterpriseservices.Globals.ControllerAPIName = "RefundsAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Refunds.ToList();
            }
            
        })
        .WithName("GetAllRefunds")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Refunds.Where(m => m.RefundId == id).ToList();
            }
        })
        .WithName("GetRefundsById")
        .WithOpenApi();
    
            //[HttpGet]
        group.MapGet("/user/{Userid}", (string Userid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Refunds.Where(m => m.Useridasstring == Userid).ToList();
            }
        })
        .WithName("GetRefundsByUserId")
        .WithOpenApi();
    
    
               //[HttpGet]
        group.MapGet("/transactions/{Refundid}", (string Refundid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Refunds.Where(m => m.TransactionId == Refundid).ToList();
            }
        })
        .WithName("GetRefundByRedundId")
        .WithOpenApi();
    
    
    

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Refund input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Refund[] someRefunds = context.Refunds.Where(m => m.RefundId == id).ToArray();
                context.Refunds.Attach(someRefunds[0]);
                if (input.CardType != null) someRefunds[0].CardType = input.CardType;
		if (input.Fullname != null) someRefunds[0].Fullname = input.Fullname;
		if (input.ParkName != null) someRefunds[0].ParkName = input.ParkName;
		if (input.State != null) someRefunds[0].State = input.State;
		someRefunds[0].ParkId = input.ParkId;


		await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.BookingId);
            }


        })
        .WithName("UpdateRefunds")
        .WithOpenApi();

        group.MapPost("/", async (Refund input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Refunds.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.RefundId);
            }

        })
        .WithName("CreateRefunds")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Refundss.Add(std);
                Refund[] someRefundss = context.Refunds.Where(m => m.RefundId == id).ToArray();
                context.Refunds.Attach(someRefundss[0]);
                context.Refunds.Remove(someRefundss[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteRefunds")
        .WithOpenApi();
    }
}

