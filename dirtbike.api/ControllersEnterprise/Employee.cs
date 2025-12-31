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
                Employee[] someEmployees = context.Employees.Where(m => m.Id == id).ToArray();
                context.Employees.Attach(someEmployees[0]);
               if (input.EmployeeId != null) someEmployees[0].EmployeeId = input.EmployeeId;
			   if (input.EmployeeTenure != null) someEmployees[0].EmployeeTenure = input.EmployeeTenure;
				if (input.EmployeeStartDate != null) someEmployees[0].EmployeeStartDate = input.EmployeeStartDate;
				if (input.EmployeeReturnDate != null) someEmployees[0].EmployeeReturnDate = input.EmployeeReturnDate;
				if (input.HrId != null) someEmployees[0].HrId = input.HrId;
				if (input.HrSystemConString != null) someEmployees[0].HrSystemConString = input.HrSystemConString;
				if (input.FullName != null) someEmployees[0].FullName = input.FullName;
				if (input.UserId != null) someEmployees[0].UserId = input.UserId;
				if (input.UserProfileId != null) someEmployees[0].UserProfileId = input.UserProfileId;
				if (input.ManagerId != null) someEmployees[0].ManagerId = input.ManagerId;
				if (input.RegionId != null) someEmployees[0].RegionId = input.RegionId;
				if (input.BuId != null) someEmployees[0].BuId = input.BuId;
				if (input.StoreId != null) someEmployees[0].StoreId = input.StoreId;
				if (input.CompanyId != null) someEmployees[0].CompanyId = input.CompanyId;
				if (input.EmployeeIdAsInt != null) someEmployees[0].EmployeeIdAsInt = input.EmployeeIdAsInt;
				if (input.EmployeeEmail != null) someEmployees[0].EmployeeEmail = input.EmployeeEmail;
				if (input.Employee1 != null) someEmployees[0].Employee1 = input.Employee1;
				if (input.PlainPassword != null) someEmployees[0].PlainPassword = input.PlainPassword;
				if (input.HashedPassword != null) someEmployees[0].HashedPassword = input.HashedPassword;
				if (input.PasswordType != null) someEmployees[0].PasswordType = input.PasswordType;
				if (input.Token != null) someEmployees[0].Token = input.Token;
				if (input.TokenProvider != null) someEmployees[0].TokenProvider = input.TokenProvider;
				if (input.ResetToken != null) someEmployees[0].ResetToken = input.ResetToken;
				if (input.TokenExpiration != null) someEmployees[0].TokenExpiration = input.TokenExpiration;
				if (input.Role != null) someEmployees[0].Role = input.Role;
				if (input.Btn != null) someEmployees[0].Btn = input.Btn;
				if (input.NcrId != null) someEmployees[0].NcrId = input.NcrId;
				if (input.AlohaId != null) someEmployees[0].AlohaId = input.AlohaId;
				if (input.OracleId != null) someEmployees[0].OracleId = input.OracleId;
				if (input.AzureId != null) someEmployees[0].AzureId = input.AzureId;
				if (input.ProfileUrl != null) someEmployees[0].ProfileUrl = input.ProfileUrl;
				if (input.IsCertified != null) someEmployees[0].IsCertified = input.IsCertified;
				if (input.GroupId1 != null) someEmployees[0].GroupId1 = input.GroupId1;
				if (input.GroupId2 != null) someEmployees[0].GroupId2 = input.GroupId2;
				if (input.GroupId3 != null) someEmployees[0].GroupId3 = input.GroupId3;
				if (input.GroupId4 != null) someEmployees[0].GroupId4 = input.GroupId4;
				if (input.GroupId5 != null) someEmployees[0].GroupId5 = input.GroupId5;
				if (input.FirstName != null) someEmployees[0].FirstName = input.FirstName;
				if (input.LastName != null) someEmployees[0].LastName = input.LastName;
				if (input.UserName != null) someEmployees[0].UserName = input.UserName;

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

