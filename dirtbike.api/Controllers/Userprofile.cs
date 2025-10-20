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


public static class UserprofileEndpoints
{
    
    public static void MapUserprofileEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Userprofile").WithTags(nameof(Userprofile));
        Enterpriseservices.Globals.ControllerAPIName = "UserprofileAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Userprofiles.ToList();
            }
            
        })
        .WithName("GetAllUserprofiles")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Userprofiles.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetUserprofileById")
        .WithOpenApi();
    
            //[HttpGet]
        group.MapGet("/user/{Useridastring}", (string Userid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETUSERWITHID", 1, "Test", "Test"); 
                return context.Userprofiles.Where(m => m.Useridasstring == Userid).ToList();
            }
        })
        .WithName("GetUserprofileByUserId")
        .WithOpenApi();
    
    
    

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Userprofile input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Userprofile[] someUserprofile = context.Userprofiles.Where(m => m.Id == id).ToArray();
                context.Userprofiles.Attach(someUserprofile[0]);
                if (input.Fullname != null) someUserprofile[0].Fullname = input.Fullname;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateUserprofile")
        .WithOpenApi();

        group.MapPost("/", async (Userprofile input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Userprofiles.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateUserprofile")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Userprofiles.Add(std);
                Userprofile[] someUserprofiles = context.Userprofiles.Where(m => m.Id == id).ToArray();
                context.Userprofiles.Attach(someUserprofiles[0]);
                context.Userprofiles.Remove(someUserprofiles[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteUserprofile")
        .WithOpenApi();
    }
}

