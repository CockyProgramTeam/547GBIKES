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

        //[HttpGet]
        group.MapGet("/", () =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETALL", 1, "TEST", "TEST");
                return context.Parks.ToList();
            }

        })
        .WithName("GetAllParkss")
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
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "UPDATEWITHID", 1, "TEST", "TEST");
                return TypedResults.Accepted("Updated ID:" + input.ParkId);
            }


        })
        .WithName("UpdateParks")
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

