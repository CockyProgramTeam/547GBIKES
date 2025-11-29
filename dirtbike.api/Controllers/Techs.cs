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


public static class NoctechsEndpoints
{
    
    public static void MapNoctechsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Noctech").WithTags(nameof(Noctech));
        Enterpriseservices.Globals.ControllerAPIName = "NoctechsAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Noctechs.ToList();
            }
            
        })
        .WithName("GetAllNoctechs")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Noctechs.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetNoctechsById")
        .WithOpenApi();

group.MapPut("/{id}", async (int id, Noctech input) => 

{ 
    using (var context = new DirtbikeContext()) 
    { 
        Noctech[] someNoctechs = context.Noctechs.Where(m => m.Id == id).ToArray(); 
        context.Noctechs.Attach(someNoctechs[0]); 
        if (input.Userid != null) someNoctechs[0].Userid = input.Userid;
        if (input.Employeeid != null) someNoctechs[0].Employeeid = input.Employeeid;
        if (input.Email != null) someNoctechs[0].Email = input.Email;
        if (input.Phone != null) someNoctechs[0].Phone = input.Phone;
        if (input.Sms != null) someNoctechs[0].Sms = input.Sms;
        if (input.Techaddress1 != null) someNoctechs[0].Techaddress1 = input.Techaddress1;
        if (input.Techaddress2 != null) someNoctechs[0].Techaddress2 = input.Techaddress2;
        if (input.Techcity != null) someNoctechs[0].Techcity = input.Techcity;
        if (input.Techstate != null) someNoctechs[0].Techstate = input.Techstate;
        if (input.Techzip != null) someNoctechs[0].Techzip = input.Techzip; 
        if (input.Fullname != null) someNoctechs[0].Fullname = input.Fullname; 
        await context.SaveChangesAsync(); 
        Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test"); 
        return TypedResults.Accepted("Updated ID:" + input.Id); 
        } 
        }) 
        .WithName("UpdateNoctechs") 
        .WithOpenApi();




        group.MapPost("/", async (Noctech input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Noctechs.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateNoctechs")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Noctechss.Add(std);
                Noctech[] someNoctechs = context.Noctechs.Where(m => m.Id == id).ToArray();
                context.Noctechs.Attach(someNoctechs[0]);
                context.Noctechs.Remove(someNoctechs[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteNoctechs")
        .WithOpenApi();
    }
}

