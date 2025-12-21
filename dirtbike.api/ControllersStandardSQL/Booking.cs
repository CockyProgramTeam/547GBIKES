using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using dirtbike.api.Models;
using dirtbike.api.Data;
using Enterpriseservices;
using Microsoft.Extensions.WebEncoders.Testing;
namespace Enterprise.Controllers;


public static class BookingEndpoints
{
    
    public static void MapBookingEndpoints(this IEndpointRouteBuilder routes)
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
        .WithName("GetAllBookings")
        .WithOpenApi();

    
          //[HttpGet]
        group.MapGet("/park/{parkId}", (int parkId) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Bookings.Where(m => m.ParkId == parkId).ToList();
            }
        })
        .WithName("GetBookingByParkId")
        .WithOpenApi();
    
    
    	//SET TO RETURN BOOKINGS FOR A GIVEN USERID WHICH IS AN INT IN EF CORE.
        //[HttpPOST]
        group.MapPost("/parkbyuser/", (BookingRequest input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Bookings.Where(m => int.Parse(m.Uid) == input.Userid).ToList();
            }
        })
        .WithName("GetBookingByUserId")
        .WithOpenApi();
    
    
    
    
    
        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Bookings.Where(m => m.BookingId == id).ToList();
            }
        })
        .WithName("GetBookingById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Booking input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Booking[] someBooking = context.Bookings.Where(m => m.BookingId == id).ToArray();
                context.Bookings.Attach(someBooking[0]);
        //SPECIFIC FIELDS WE WANT TO UPDATE -> ALL FIELDS FOR THIS ENDPOINT 
		if (input.CustomerBillingName != null) someBooking[0].CustomerBillingName = input.CustomerBillingName;
		someBooking[0].BookingId = input.BookingId;
		if (input.Uid != null) someBooking[0].Uid = input.Uid;
		if (input.BillingTelephoneNumber != null) someBooking[0].BillingTelephoneNumber = input.BillingTelephoneNumber;
		if (input.CreditCardType != null) someBooking[0].CreditCardType = input.CreditCardType;
		if (input.CreditCardLast4 != null) someBooking[0].CreditCardLast4 = input.CreditCardLast4;
		if (input.CreditCardExpDate != null) someBooking[0].CreditCardExpDate = input.CreditCardExpDate;
		if (input.QuantityAdults != null) someBooking[0].QuantityAdults = input.QuantityAdults;
		if (input.QuantityChildren != null) someBooking[0].QuantityChildren = input.QuantityChildren;
		if (input.TotalAmount != null) someBooking[0].TotalAmount = input.TotalAmount;
		if (input.TransactionId != null) someBooking[0].TransactionId = input.TransactionId;
		if (input.ParkId != null) someBooking[0].ParkId = input.ParkId;
		if (input.ParkName != null) someBooking[0].ParkName = input.ParkName;
		if (input.Cartid != null) someBooking[0].Cartid = input.Cartid;
		if (input.Reservationtype != null) someBooking[0].Reservationtype = input.Reservationtype;
		if (input.Reservationstatus != null) someBooking[0].Reservationstatus = input.Reservationstatus;
		if (input.Reversetransactionid != null) someBooking[0].Reversetransactionid = input.Reversetransactionid;
		if (input.Cancellationrefund != null) someBooking[0].Cancellationrefund = input.Cancellationrefund;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.BookingId);
            }


        })
        .WithName("UpdateBooking")
        .WithOpenApi();

group.MapPost("/", async (Booking input) =>
{
    using (var context = new DirtbikeContext())
    {
        context.Bookings.Add(input);
        await context.SaveChangesAsync();

        Enterpriseservices.ApiLogger.logapi(
            Enterpriseservices.Globals.ControllerAPIName,
            Enterpriseservices.Globals.ControllerAPINumber,
            "NEWRECORD",
            1,
            "TEST",
            "TEST"
        );

        var notifier = new Enterpriseservices.EmailNotifiers();

        string emailmsg =
            $"547Bikes Reservation Created for Park {input.ParkName} + {input.TransactionId} + {DateTime.Today:MM/dd/yyyy}";

        // Determine which email to use
        string emailToUse = input.Emailnoticeaddress;

        // Validate email and fall back if invalid
        try
        {
            if (string.IsNullOrWhiteSpace(emailToUse))
            {
                throw new FormatException("Email is empty");
            }

            // Throws if invalid
            var _ = new System.Net.Mail.MailAddress(emailToUse);
        }
        catch
        {
            // Fallback email
            emailToUse = "stritzj@email.sc.edu";

            Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "EMAIL NOTIFICATION", 1, "Test", "Test");
        }

        // BEST EFFORT EMAIL SEND
        try
        {
            await notifier.gmailsendnotificationasync(
                input.Userid ?? 0,
                emailToUse,
                emailmsg
            );
        }
        catch (Exception ex)
        {
        Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "EMAILNOTIFICATION-LASTRESORT", 1, "Test", "Test");
        }

        return TypedResults.Created("Created ID:" + input.BookingId);
    }
})
.WithName("CreateBooking")
.WithOpenApi();


                

    
            group.MapPost("/park/{parkId}", async (int parkId, Booking input) =>
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
        .WithName("CreateBookingWithParkId")
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
        .WithName("DeleteBooking")
        .WithOpenApi();
    }
}

public class BookingRequest { public string Parkname { get; set; } public int Userid { get; set; } }
