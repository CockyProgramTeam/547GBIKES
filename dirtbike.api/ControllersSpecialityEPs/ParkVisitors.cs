using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Mail;
using dirtbike.api.Data;
using dirtbike.api.Models;
using Enterpriseservices;
namespace Enterprise.Controllers;

//THESE INPUTS ARE SPECIALITY ENDPOINTS REQUIRED FOR MANAGING PARK RESERVATIONS, NOT FROM A USER PERSPECTIVE
//BUT FROM THE PARKS PERSPECTIVE.
//PARK STORES THE TOTAL NUMBER OF USERS ALREADY IN PLACE.
//PARK RESERVATIONS SHOULD REJECT BOOKINGS WHICH OVERSUBSUBSCRIBE THE PARK.
//THE PARK GETTER RETURNS THE CURRENT PARK TOTALS, MAXIMUMS AND ADULTS VS CHILDREN AS INVENTORY. SO NO 
//CALLS WILL BE MADE FOR THESE INPUTS.
//THE CG UI THEREFORE HAS WHAT IT NEEDS WHEN THE PARK CG IS CALLED.
//WE ARE GOING TO ADD A RESERVATION VALIDATION ANYWAY AS WE MIGHT NEED IT IN SOME SUBSCREENS.

public static class ParksCalendarSpeciality
{

    public static void MapParkInventoryEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/ParkInventory").WithTags(nameof(Park));
        Globals.ControllerAPIName = "ParksInventoryAPI";
        Globals.ControllerAPINumber = "100"; //SPECIALITY CONTROLLERS STARTING AT 100.


           //[HttpPut]
        group.MapPut("/removeguests/", async (int park, int Removesomeguests) =>
        {
            using (var context = new DirtbikeContext())
            {
                Park[] someParks = context.Parks.Where(m => m.ParkId == park).ToArray();
                context.Parks.Attach(someParks[0]);
             	int temp = someParks[0].Currentvisitors;
                someParks[0].Currentvisitors = someParks[0].Currentvisitors - Removesomeguests;
            	await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "REMOVEGUESTS", 1, "TEST", "TEST");
                return TypedResults.Accepted("Updated ParkID: " + park + "Previous Visitors: " + temp + "CurrentVisitors: " + someParks[0].Currentvisitors);
            }
        })
        .WithName("RemoveSomeParkGuests")
        .WithOpenApi();

    group.MapPut("/addguests/", async (int park, int Addsomeguests) =>
        {
            using (var context = new DirtbikeContext())
            {
                Park[] someParks = context.Parks.Where(m => m.ParkId == park).ToArray();
                context.Parks.Attach(someParks[0]);
                int temp = someParks[0].Currentvisitors;
             	someParks[0].Currentvisitors = someParks[0].Currentvisitors + Addsomeguests;
            	await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "ADDGUESTS", 1, "TEST", "TEST");
                return TypedResults.Accepted("Updated ParkID: " + park + "Previous Visitors: " + temp + "CurrentVisitors: " + someParks[0].Currentvisitors);
            }
        })
        .WithName("AddSomeParkGuests")
        .WithOpenApi();

        group.MapPut("/calendarentry/", async (ParkCalendar input) =>
        {
            using (var context = new DirtbikeContext())
            {
                context.ParkCalendars.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "ADDCALENDARENTRYPARK", 1, "TEST", "TEST");
                return TypedResults.Accepted("NewPark Reservation Added for: " + input.ParkId);
            }
        })
        .WithName("AddGuestCalendar")
        .WithOpenApi();

      group.MapGet("/capacitycheck/", (int park, int addGuests) =>
        {
        using var context = new DirtbikeContext();

        var parkEntity = context.Parks.FirstOrDefault(m => m.ParkId == park);
        if (parkEntity == null)
        {
        // Park not found → treat as no capacity
        return 0;
        }

    // Normalize null visitor count to zero
        if (parkEntity.Currentvisitors == null)
        {
        parkEntity.Currentvisitors = 0;
        }

        int newVisitorCount = 0;
        newVisitorCount = parkEntity.Currentvisitors + addGuests;

        Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "ADDGUESTS_CAPACITY", 1, "TEST", "TEST");

    // Use a standard if block
        if (newVisitorCount <= parkEntity.Maxvisitors)
        {
        return 1;
        }
        else
        {
        return 0;
        }
    })
    .WithName("CheckReservationCapacity")
    .WithOpenApi();
    }
}
