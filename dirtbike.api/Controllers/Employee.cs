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


public static class EmployeeEndpoints
{
    
    public static void MapEmployeeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Employee").WithTags(nameof(Employee));
        Enterpriseservices.Globals.ControllerAPIName = "EmployeeAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Employees.ToList();
            }
            
        })
        .WithName("GetAllEmployees")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Employees.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetEmployeeById")
        .WithOpenApi();

        group.MapGet("/userid/{Userid}", (string Userid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Employees.Where(m => m.EmployeeId == Userid).ToList();
            }
        })
        .WithName("GetEmployeeByUserIdString")
        .WithOpenApi();



        //[HttpPut]
        group.MapPut("/{id}", async (int id, Employee input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Employee[] someEmployee = context.Employees.Where(m => m.Id == id).ToArray();
                context.Employees.Attach(someEmployee[0]);
                if (input.FullName != null) someEmployee[0].FullName = input.FullName;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.EmployeeId);
            }


        })
        .WithName("UpdateEmployee")
        .WithOpenApi();

        group.MapPost("/", async (Employee input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Employees.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.EmployeeId);
            }

        })
        .WithName("CreateEmployee")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Employees.Add(std);
                Employee[] someEmployees = context.Employees.Where(m => m.Id == id).ToArray();
                context.Employees.Attach(someEmployees[0]);
                context.Employees.Remove(someEmployees[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteEmployee")
        .WithOpenApi();
    }
}

