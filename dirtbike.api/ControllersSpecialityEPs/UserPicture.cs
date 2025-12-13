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


public static class UserPictureEndpoints
{
    
    public static void MapUserPictureEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Userpicture").WithTags(nameof(UserPicture));
        Enterpriseservices.Globals.ControllerAPIName = "UserPictureAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.UserPictures.ToList();
            }
            
        })
        .WithName("GetAllUserPictures")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.UserPictures.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetUserPictureById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, UserPicture input) =>
        {
            using (var context = new DirtbikeContext())
            {
                UserPicture[] someUserPicture = context.UserPictures.Where(m => m.Id == id).ToArray();
                context.UserPictures.Attach(someUserPicture[0]);
                if (input.Activepictureurl != null) someUserPicture[0].Activepictureurl = input.Activepictureurl;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateUserPicture")
        .WithOpenApi();

        group.MapPost("/", async (UserPicture input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.UserPictures.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateUserPicture")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.UserPictures.Add(std);
                UserPicture[] someUserPictures = context.UserPictures.Where(m => m.Id == id).ToArray();
                context.UserPictures.Attach(someUserPictures[0]);
                context.UserPictures.Remove(someUserPictures[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteUserPicture")
        .WithOpenApi();
    }
}

