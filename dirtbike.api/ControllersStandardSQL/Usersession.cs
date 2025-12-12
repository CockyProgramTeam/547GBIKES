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


public static class UsersessionEndpoints
{
    
    public static void MapUsersessionEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Usersession").WithTags(nameof(Usersession));
        Enterpriseservices.Globals.ControllerAPIName = "UsersessionAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Usersessions.ToList();
            }
            
        })
        .WithName("GetAllUsersessions")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Usersessions.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetUsersessionById")
        .WithOpenApi();
    
            //[HttpGet]
        group.MapGet("/user/{Useridasstring}", (string Uid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Usersessions.Where(m => m.Useridasstring == Uid).ToList();
            }
        })
        .WithName("GetUsersessionByUserId")
        .WithOpenApi();
    
    
    
    

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Usersession input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Usersession[] someUsersession = context.Usersessions.Where(m => m.Id == id).ToArray();
                context.Usersessions.Attach(someUsersession[0]);
                if (input.Sessiondescription != null) someUsersession[0].Sessiondescription = input.Sessiondescription;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateUsersession")
        .WithOpenApi();

        group.MapPost("/", async (Usersession input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Usersessions.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateUsersession")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Usersessions.Add(std);
                Usersession[] someUsersessions = context.Usersessions.Where(m => m.Id == id).ToArray();
                context.Usersessions.Attach(someUsersessions[0]);
                context.Usersessions.Remove(someUsersessions[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteUsersession")
        .WithOpenApi();
    }
}

