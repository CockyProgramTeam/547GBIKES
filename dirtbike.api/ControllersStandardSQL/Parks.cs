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
public static class ParksEndpoints
{

    public static void MapParksEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Parks").WithTags(nameof(Park));
        Globals.ControllerAPIName = "ParksAPI";
        Globals.ControllerAPINumber = "002";

  group.MapGet("/", () =>
{
    using var context = new DirtbikeContext();

    Enterpriseservices.ApiLogger.logapi(
        Enterpriseservices.Globals.ControllerAPIName,
        Enterpriseservices.Globals.ControllerAPINumber,
        "GETALL", 1, "TEST", "TEST"
    );

    return context.Parks.ToList();
})
.WithName("GetAllParks")
.WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "TEST", "TEST");
                return context.Parks.Where(m => m.ParkId == id).ToList();
            }
        })
        .WithName("GetParksById")
        .WithOpenApi();

        

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Park input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Park[] someParks = context.Parks.Where(m => m.ParkId == id).ToArray();
                context.Parks.Attach(someParks[0]);
                if (input.Description != null) someParks[0].Description = input.Description;
                if (input.DayPassPriceUsd != null) someParks[0].DayPassPriceUsd = input.DayPassPriceUsd;
                if (input.Longitude != null) someParks[0].Longitude = input.Longitude;
                if (input.Latitude != null) someParks[0].Latitude = input.Latitude;
                if (input.Difficulty != null) someParks[0].Difficulty = input.Difficulty;
                if (input.TrailLengthMiles != null) someParks[0].TrailLengthMiles = input.TrailLengthMiles;
                if (input.Name != null) someParks[0].Name = input.Name;
                if (input.Region != null) someParks[0].Region = input.Region;
                if (input.Phone != null) someParks[0].Phone = input.Phone;
                if (input.Address != null) someParks[0].Address = input.Address;
                if (input.Trailmapurl != null) someParks[0].Trailmapurl = input.Trailmapurl;
                if (input.Parklogourl != null) someParks[0].Parklogourl = input.Parklogourl;
                if (input.State != null) someParks[0].State = input.State;
            	if (input.State != null) someParks[0].State = input.State;
            	if (input.Pic1url != null) someParks[0].Pic1url = input.Pic1url;
				if (input.Pic2url != null) someParks[0].Pic2url = input.Pic2url;
				if (input.Pic3url != null) someParks[0].Pic3url = input.Pic3url;
				if (input.Pic4url != null) someParks[0].Pic4url = input.Pic4url;
				if (input.Pic5url != null) someParks[0].Pic5url = input.Pic5url;
				if (input.Pic6url != null) someParks[0].Pic6url = input.Pic6url;
				if (input.Pic7url != null) someParks[0].Pic7url = input.Pic7url;
				if (input.Pic8url != null) someParks[0].Pic8url = input.Pic8url;
				if (input.Pic9url != null) someParks[0].Pic9url = input.Pic9url;
				if (input.Isnationalpark != null) someParks[0].Isnationalpark = input.Isnationalpark;
				if (input.Isstatepark != null) someParks[0].Isstatepark = input.Isstatepark;
				if (input.Hqbranchid != null) someParks[0].Hqbranchid = input.Hqbranchid;
				if (input.Mountainbikes != null) someParks[0].Mountainbikes = input.Mountainbikes;
				if (input.Camping != null) someParks[0].Camping = input.Camping;
				if (input.Rafting != null) someParks[0].Rafting = input.Rafting;
				if (input.Canoeing != null) someParks[0].Canoeing = input.Canoeing;
				if (input.Frisbee != null) someParks[0].Frisbee = input.Frisbee; 
				if (input.Tents != null) someParks[0].Tents = input.Tents; 
				if (input.Cabins != null) someParks[0].Cabins = input.Cabins; 
				if (input.Motocross != null) someParks[0].Motocross = input.Motocross; 
				if (input.Skiing != null) someParks[0].Skiing = input.Skiing;
                someParks[0].AverageRating = input.AverageRating;

                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "UPDATEWITHID", 1, "TEST", "TEST");
                return TypedResults.Accepted("Updated ID:" + input.ParkId);
            }


        })
        .WithName("UpdateParks")
        .WithOpenApi();
    
    
           //[HttpPut]
        group.MapPut("/guests/{Limit}", async (int park, int Limit) =>
        {
            using (var context = new DirtbikeContext())
            {
                Park[] someParks = context.Parks.Where(m => m.ParkId == park).ToArray();
                context.Parks.Attach(someParks[0]);
                someParks[0].Maxvisitors = Limit;
				await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "SETLIMITS", 1, "TEST", "TEST");
                return TypedResults.Accepted("Updated ParkID: " + park);
            }
        })
        .WithName("SetParkLimits")
        .WithOpenApi();
        
 

        group.MapPost("/", async (Park input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Parks.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "CREATENEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.ParkId);
            }

        })
        .WithName("CreateParks")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Parkss.Add(std);
                Park[] someParkss = context.Parks.Where(m => m.ParkId == id).ToArray();
                context.Parks.Attach(someParkss[0]);
                context.Parks.Remove(someParkss[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID", 1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteParks")
        .WithOpenApi();
    }
}

