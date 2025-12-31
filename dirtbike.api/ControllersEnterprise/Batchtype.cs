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

public static class BatchtypeEndpoints
{

    public static void MapBatchtypeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Batchtype").WithTags(nameof(Batchtype));

        //[HttpGet]
        group.MapGet("/", () =>
        {
            using (var context = new DirtbikeContext())
            {
                return context.Batchtypes.ToList();
            }

        })
        .WithName("GetAllBatchtypes")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                return context.Batchtypes.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetBatchtypeById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Batchtype input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Batchtype[] someBatchtype = context.Batchtypes.Where(m => m.Id == id).ToArray();
                context.Batchtypes.Attach(someBatchtype[0]);
                someBatchtype[0].Batchtypename = input.Batchtypename;
                await context.SaveChangesAsync();
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateBatchtype")
        .WithOpenApi();

        group.MapPost("/", async (Batchtype input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Batchtypes.Add(input);
                await context.SaveChangesAsync();
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateBatchtype")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Batchtypes.Add(std);
                Batchtype[] someBatchtypes = context.Batchtypes.Where(m => m.Id == id).ToArray();
                context.Batchtypes.Attach(someBatchtypes[0]);
                context.Batchtypes.Remove(someBatchtypes[0]);
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteBatchtype")
        .WithOpenApi();
    }
}

