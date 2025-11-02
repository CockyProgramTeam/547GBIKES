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

public static class LearnlogEndpoints
{

    public static async void MapLearnlogEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Learndetail").WithTags(nameof(Learnlog));

        //[HttpGet]
        group.MapGet("/", () =>
        {
            using (var context = new DirtbikeContext())
            {
                return context.Learnlogs.ToList();
            }

        })
        .WithName("GetAllLearnlogs")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/userid/{userid}", (int userid) =>
        {
            using (var context = new DirtbikeContext())
            {
                return context.Learnlogs.Where(m => m.Userid == userid).ToList();
            }
        })
        .WithName("GetLearnlogByUserId")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/id/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                return context.Learnlogs.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetLearnlogByLearnLogId")
        .WithOpenApi();






        //[HttpPut]
        group.MapPut("/{id}", (int id, Learnlog input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Learnlog[] someLearnlog = context.Learnlogs.Where(m => m.Id == id).ToArray();
                context.Learnlogs.Attach(someLearnlog[0]);
                someLearnlog[0].Description = input.Description;
                someLearnlog[0].Userid = input.Userid;
                someLearnlog[0].Employeeidasint = input.Employeeidasint;
                someLearnlog[0].Employee = input.Employee;
                someLearnlog[0].Employeeid = input.Employeeid;
                someLearnlog[0].Learningmodulesid = input.Learningmodulesid;
                someLearnlog[0].Cataloguesku = input.Cataloguesku;
                context.SaveChangesAsync();
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateLearnlog")
        .WithOpenApi();

        group.MapPost("/", async (Learnlog input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Learnlogs.Add(input);
                await context.SaveChangesAsync();
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateLearnlog")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Learnlogs.Add(std);
                Learnlog[] someLearnlogs = context.Learnlogs.Where(m => m.Id == id).ToArray();
                context.Learnlogs.Attach(someLearnlogs[0]);
                context.Learnlogs.Remove(someLearnlogs[0]);
                context.SaveChangesAsync();
            }

        })
        .WithName("DeleteLearnlog")
        .WithOpenApi();
    }
}

