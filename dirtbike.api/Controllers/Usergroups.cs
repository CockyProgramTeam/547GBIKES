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

public static class UsergroupEndpoints
{

    public static async void MapUsergroupsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Usergroups").WithTags(nameof(Usergroup));

        //[HttpGet]
        group.MapGet("/", () =>
        {
            using (var context = new DirtbikeContext())
            {
                return context.Usergroups.ToList();
            }

        })
        .WithName("GetAllUsergroups")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                return context.Usergroups.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetUsergroupsById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", (int id, Usergroup input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Usergroup[] someUsergroups = context.Usergroups.Where(m => m.Id == id).ToArray();
                context.Usergroups.Attach(someUsergroups[0]);
                someUsergroups[0].Groupdescription = input.Groupdescription;
                context.SaveChangesAsync();
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateUsergroups")
        .WithOpenApi();

        group.MapPost("/", async (Usergroup input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Usergroups.Add(input);
                await context.SaveChangesAsync();
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateUsergroups")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Usergroups.Add(std);
                Usergroup[] someUsergroups = context.Usergroups.Where(m => m.Id == id).ToArray();
                context.Usergroups.Attach(someUsergroups[0]);
                context.Usergroups.Remove(someUsergroups[0]);
                context.SaveChangesAsync();
            }

        })
        .WithName("DeleteUsergroups")
        .WithOpenApi();
    }
}

