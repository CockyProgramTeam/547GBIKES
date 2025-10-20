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


public static class CompanyEndpoints
{
    
    public static void MapCompanyEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Company").WithTags(nameof(Company));
        Enterpriseservices.Globals.ControllerAPIName = "CompanyAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Companies.ToList();
            }
            
        })
        .WithName("GetAllCompanys")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Companies.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetCompanyById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Company input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Company[] someCompany = context.Companies.Where(m => m.Id == id).ToArray();
                context.Companies.Attach(someCompany[0]);
                if (input.Companyname != null) someCompany[0].Companyname = input.Companyname;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateCompany")
        .WithOpenApi();

        group.MapPost("/", async (Company input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Companies.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateCompany")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Companys.Add(std);
                Company[] someCompanys = context.Companies.Where(m => m.Id == id).ToArray();
                context.Companies.Attach(someCompanys[0]);
                context.Companies.Remove(someCompanys[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteCompany")
        .WithOpenApi();
    }
}

