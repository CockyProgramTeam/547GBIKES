using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http;
using dirtbike.api.Models;
using dirtbike.api.Data;
namespace Enterprise.Controllers;

public static class UseractionEndpoints
{

    public static void MapUseractionEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Useraction").WithTags(nameof(Useraction));

        //[HttpGet]
        group.MapGet("/", () =>
        {
            using (var context = new DirtbikeContext())
            {
                return context.Useractions.ToList();
            }

        })
        .WithName("GetAllUseractions")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                return context.Useractions.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetUseractionById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Useraction input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Useraction[] someUseraction = context.Useractions.Where(m => m.Id == id).ToArray();
                context.Useractions.Attach(someUseraction[0]);
                someUseraction[0].Description = input.Description;
                await context.SaveChangesAsync();
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateUseraction")
        .WithOpenApi();

        group.MapPost("/", async (Useraction input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Useractions.Add(input);
                await context.SaveChangesAsync();
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateUseraction")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Useractions.Add(std);
                Useraction[] someUseractions = context.Useractions.Where(m => m.Id == id).ToArray();
                context.Useractions.Attach(someUseractions[0]);
                context.Useractions.Remove(someUseractions[0]);
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteUseraction")
        .WithOpenApi();
    }
}

