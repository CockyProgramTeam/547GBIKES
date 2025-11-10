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


public static class PaymentsEndpoints
{
    
    public static void MapPaymentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Payments").WithTags(nameof(Payment));
        Enterpriseservices.Globals.ControllerAPIName = "PaymentsAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Payments.ToList();
            }
            
        })
        .WithName("GetAllPaymentss")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Payments.Where(m => m.PaymentId == id).ToList();
            }
        })
        .WithName("GetPaymentsById")
        .WithOpenApi();
    
            //[HttpGet]
        group.MapGet("/user/{Userid}", (string Userid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Payments.Where(m => m.Useridasstring == Userid).ToList();
            }
        })
        .WithName("GetPaymentsByUserId")
        .WithOpenApi();
    
    
               //[HttpGet]
        group.MapGet("/transactions/{Paymentid}", (string Paymentid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Payments.Where(m => m.TransactionId == Paymentid).ToList();
            }
        })
        .WithName("GetTransactionByTransactionId")
        .WithOpenApi();
    
    
    

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Payment input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Payment[] somePayments = context.Payments.Where(m => m.PaymentId == id).ToArray();
                context.Payments.Attach(somePayments[0]);
                if (input.CardType != null) somePayments[0].CardType = input.CardType;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.BookingId);
            }


        })
        .WithName("UpdatePayments")
        .WithOpenApi();

        group.MapPost("/", async (Payment input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Payments.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.PaymentId);
            }

        })
        .WithName("CreatePayments")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Paymentss.Add(std);
                Payment[] somePaymentss = context.Payments.Where(m => m.PaymentId == id).ToArray();
                context.Payments.Attach(somePaymentss[0]);
                context.Payments.Remove(somePaymentss[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeletePayments")
        .WithOpenApi();
    }
}

