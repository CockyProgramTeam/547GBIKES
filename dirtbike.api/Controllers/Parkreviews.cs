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


public static class ParkReviewEndpoints
{
    
    public static void MapParkReviewEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/ParkReview").WithTags(nameof(ParkReview));
        Enterpriseservices.Globals.ControllerAPIName = "ParkReviewAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.ParkReviews.ToList();
            }
            
        })
        .WithName("GetAllParkReviews")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.ParkReviews.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetParkReviewById")
        .WithOpenApi();
    
       //[HttpGet]
        group.MapGet("/user/{Useridasstring}", (string Userid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.ParkReviews.Where(m => m.Useridasstring == Userid).ToList();
            }
        })
        .WithName("GetParkReviewByUserId")
        .WithOpenApi();
    
    
    
    

        //[HttpPut]
        group.MapPut("/{id}", async (int id, ParkReview input) =>
        {
            using (var context = new DirtbikeContext())
            {
                ParkReview[] someParkReview = context.ParkReviews.Where(m => m.Id == id).ToArray();
                context.ParkReviews.Attach(someParkReview[0]);
                if (input.Description != null) someParkReview[0].Description = input.Description;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateParkReview")
        .WithOpenApi();

        group.MapPost("/", async (ParkReview input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.ParkReviews.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateParkReview")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.ParkReviews.Add(std);
                ParkReview[] someParkReviews = context.ParkReviews.Where(m => m.Id == id).ToArray();
                context.ParkReviews.Attach(someParkReviews[0]);
                context.ParkReviews.Remove(someParkReviews[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteParkReview")
        .WithOpenApi();
    }
}

