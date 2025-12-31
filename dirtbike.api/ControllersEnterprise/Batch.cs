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

public static class BatchEndpoints
{

    public static void MapBatchEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Batch").WithTags(nameof(Batch));

        //[HttpGet]
        group.MapGet("/", () =>
        {
            using (var context = new DirtbikeContext())
            {
                return context.Batches.ToList();
            }

        })
        .WithName("GetAllBatches")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                return context.Batches.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetBatchById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Batch input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Batch[] someBatch = context.Batches.Where(m => m.Id == id).ToArray();
                context.Batches.Attach(someBatch[0]);
                someBatch[0].Batchname = input.Batchname;
            someBatch[0].Filelocationpath = input.Filelocationpath;
            someBatch[0].Batchtype = input.Batchtype;
            someBatch[0].Batchstatus = input.Batchstatus;
            someBatch[0].Batchend = input.Batchend;
            someBatch[0].Qtystart = input.Qtystart;
            someBatch[0].Qtyend = input.Qtyend;
                await context.SaveChangesAsync();
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateBatch")
        .WithOpenApi();

        group.MapPost("/", async (Batch input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Batches.Add(input);
                await context.SaveChangesAsync();
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateBatch")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Batches.Add(std);
                Batch[] someBatches = context.Batches.Where(m => m.Id == id).ToArray();
                context.Batches.Attach(someBatches[0]);
                context.Batches.Remove(someBatches[0]);
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteBatch")
        .WithOpenApi();
    }
}

