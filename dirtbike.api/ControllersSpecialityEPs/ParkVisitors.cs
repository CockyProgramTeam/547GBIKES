using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using dirtbike.api.Data;
using dirtbike.api.Models;
using Enterpriseservices;

namespace Enterprise.Controllers
{
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

    
    //ADD GUESTS BY GUID
      group.MapPut("/addguestsguid/", async (string park, int Addsomeguests) =>
        {
            using (var context = new DirtbikeContext())
            {
                Park[] someParks = context.Parks.Where(m => m.Id == park).ToArray();
                context.Parks.Attach(someParks[0]);
                int temp = someParks[0].Currentvisitors;
             	someParks[0].Currentvisitors = someParks[0].Currentvisitors + Addsomeguests;
            	await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "ADDGUESTS", 1, "TEST", "TEST");
                return TypedResults.Accepted("Updated ParkID: " + park + "Previous Visitors: " + temp + "CurrentVisitors: " + someParks[0].Currentvisitors);
            }
        })
        .WithName("AddSomeParkGuestsByGuid")
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
        return -99;
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
        
      
         
        
        
   
            group.MapGet("/currentusers/", (int park) =>
            {
                using var context = new DirtbikeContext();
                var parkEntity = context.Parks.FirstOrDefault(m => m.ParkId == park);
                if (parkEntity == null) return "-99";
                if (parkEntity.Currentvisitors == null) parkEntity.Currentvisitors = 0;

                Enterpriseservices.ApiLogger.logapi(Globals.ControllerAPIName, Globals.ControllerAPINumber, "ADDGUESTS_CAPACITY", 1, "TEST", "TEST");
                return $"{parkEntity.Maxvisitors} / {parkEntity.Currentvisitors}";
            })
            .WithName("CheckCurrentUsers")
            .WithOpenApi();

            group.MapGet("/currentusersbyguid/", (string ParkGuid) =>
            {
                using var context = new DirtbikeContext();
                var parkEntity = context.Parks.FirstOrDefault(m => m.Id == ParkGuid);
                if (parkEntity == null) return "-99";
                if (parkEntity.Currentvisitors == null) parkEntity.Currentvisitors = 0;

                Enterpriseservices.ApiLogger.logapi(Globals.ControllerAPIName, Globals.ControllerAPINumber, "ADDGUESTS_CAPACITY", 1, "TEST", "TEST");
                return $"{parkEntity.Maxvisitors} / {parkEntity.Currentvisitors}";
            })
            .WithName("CheckCurrentUsersByGUID")
            .WithOpenApi();
        }

        public class ParkCapacityPayload
        {
            public int ParkId { get; set; }
            public string Id { get; set; }
            public int Maxvisitors { get; set; }
            public int Maxcampsites { get; set; }
            public int Currentvisitors { get; set; }
            public int Currentcampsites { get; set; }
            public int Currentadults { get; set; }
            public int Currentchildren { get; set; }
        }
    }
}
