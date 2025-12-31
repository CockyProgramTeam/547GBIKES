/*using System;
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


public static class TestBookingEndpoints
{
    
    public static void MapTestBookingEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Booking").WithTags(nameof(Booking));
        Enterpriseservices.Globals.ControllerAPIName = "BookingAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Bookings.ToList();
            }
            
        })
        .WithName("GetAllTestBookings")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHBookingID", 1, "Test", "Test"); 
                return context.Bookings.Where(m => m.BookingId == id).ToList();
            }
        })
        .WithName("GetTestBookingById")
        .WithOpenApi();
    
           //[HttpGet]
       
        group.MapGet("/user/{uid}", async (string uid) =>
		{
    		using (var context = new DirtbikeContext())
    		{
            Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHUSERID", 1, "Test", "Test"); 
        	var bookings = await context.Bookings.Where(m => m.Uid == uid).ToListAsync();
        	if (bookings.Count == 0) return Results.NotFound();
            return Results.Ok(bookings);
    		}
		})       
        .WithName("GetTestBookingByUserId")
        .WithOpenApi();
    
    
    
    

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Booking input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Booking[] someBooking = context.Bookings.Where(m => m.BookingId == id).ToArray();
                context.Bookings.Attach(someBooking[0]);
                if (input.CustomerBillingName != null) someBooking[0].CustomerBillingName = input.CustomerBillingName;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.BookingId);
            }


        })
        .WithName("UpdateTestBooking")
        .WithOpenApi();

        group.MapPost("/", async (Booking input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Bookings.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.BookingId);
            }

        })
        .WithName("CreateTestBooking")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Bookings.Add(std);
                Booking[] someBookings = context.Bookings.Where(m => m.BookingId == id).ToArray();
                context.Bookings.Attach(someBookings[0]);
                context.Bookings.Remove(someBookings[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteTestBooking")
        .WithOpenApi();
    }
}
*/
