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


public static class CardEndpoints
{
    
    public static void MapCardEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Card").WithTags(nameof(Card));
        Enterpriseservices.Globals.ControllerAPIName = "CardAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Cards.ToList();
            }
            
        })
        .WithName("GetAllCards")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Cards.Where(m => m.CardId == id).ToList();
            }
        })
        .WithName("GetCardById")
        .WithOpenApi();
    
    
        //[HttpGet]
        group.MapGet("/user/{Uid}", (string Userid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHUSERID", 1, "Test", "Test"); 
                return context.Cards.Where(m => m.Uid == Userid).ToList();
            }
        })
        .WithName("GetCardByUserId")
        .WithOpenApi();
    
    
    

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Card input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Card[] someCard = context.Cards.Where(m => m.CardId == id).ToArray();
                context.Cards.Attach(someCard[0]);
                if (input.CardType != null) someCard[0].CardType = input.CardType;
				if (input.CardVendor != null) someCard[0].CardVendor = input.CardVendor;
				if (input.CardLast4 != null) someCard[0].CardLast4 = input.CardLast4;
				if (input.CardExpDate != null) someCard[0].CardExpDate = input.CardExpDate;
				if (input.BillingZip != null) someCard[0].BillingZip = input.BillingZip;
				if (input.IsActive != null) someCard[0].IsActive = input.IsActive;
				if (input.Cardbtn != null) someCard[0].Cardbtn = input.Cardbtn;
				if (input.Fullname != null) someCard[0].Fullname = input.Fullname;

                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.CardId);
            }


        })
        .WithName("UpdateCard")
        .WithOpenApi();

        group.MapPost("/", async (Card input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Cards.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.CardId);
            }

        })
        .WithName("CreateCard")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Cards.Add(std);
                Card[] someCards = context.Cards.Where(m => m.CardId == id).ToArray();
                context.Cards.Attach(someCards[0]);
                context.Cards.Remove(someCards[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteCard")
        .WithOpenApi();
    }
}

