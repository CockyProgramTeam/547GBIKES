using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using dirtbike.api.Models;
using dirtbike.api.Data;
using Enterpriseservices;
using Microsoft.Extensions.WebEncoders.Testing;
namespace Enterprise.Controllers;


public static class UserlogEndpoints
{
    
    public static void MapUserlogEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Userlog").WithTags(nameof(Userlog));
        Enterpriseservices.Globals.ControllerAPIName = "UserlogAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Userlogs.ToList();
            }
            
        })
        .WithName("GetAllUserlogs")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Userlogs.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetUserlogById")
        .WithOpenApi();
    
          //[HttpGet]
        group.MapGet("/user/{Username}", (string Uid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Userlogs.Where(m => m.Username == Uid).ToList();
            }
        })
        .WithName("GetUserlogByUserId")
        .WithOpenApi();
    
    
    

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Userlog input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Userlog[] someUserlog = context.Userlogs.Where(m => m.Id == id).ToArray();
                context.Userlogs.Attach(someUserlog[0]);
                someUserlog[0].Username = input.Username;
               
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateUserlog")
        .WithOpenApi();

        group.MapPost("/", async (Userlog input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Userlogs.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateUserlog")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Userlogs.Add(std);
                Userlog[] someUserlogs = context.Userlogs.Where(m => m.Id == id).ToArray();
                context.Userlogs.Attach(someUserlogs[0]);
                context.Userlogs.Remove(someUserlogs[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteUserlog")
        .WithOpenApi();
    }
}

