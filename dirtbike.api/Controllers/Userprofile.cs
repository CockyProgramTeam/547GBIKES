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
             	var existingProfile = context.Userprofiles.FirstOrDefault(m => m.Userid == id);
            	                    
             // Update all fields if not null
        if (input.Fullname != null) existingProfile.Fullname = input.Fullname;
        if (input.Firstname != null) existingProfile.Firstname = input.Firstname;
        if (input.Lastname != null) existingProfile.Lastname = input.Lastname;
        if (input.Address1 != null) existingProfile.Address1 = input.Address1;
        if (input.Address2 != null) existingProfile.Address2 = input.Address2;
        if (input.City != null) existingProfile.City = input.City;
        if (input.Stateregion != null) existingProfile.Stateregion = input.Stateregion;
        if (input.Country != null) existingProfile.Country = input.Country;
        if (input.Phone != null) existingProfile.Phone = input.Phone;
        if (input.Cellphone != null) existingProfile.Cellphone = input.Cellphone;
        existingProfile.Sms = input.Sms;
        if (input.Email != null) existingProfile.Email = input.Email;
        if (input.Maritalstatus != null) existingProfile.Maritalstatus = input.Maritalstatus;
        if (input.University1 != null) existingProfile.University1 = input.University1;
        if (input.University2 != null) existingProfile.University2 = input.University2;
        if (input.Linkedinurl != null) existingProfile.Linkedinurl = input.Linkedinurl;
        if (input.Instagramurl != null) existingProfile.Instagramurl = input.Instagramurl;
        if (input.Vimeourl != null) existingProfile.Vimeourl = input.Vimeourl;
        if (input.Facebookurl != null) existingProfile.Facebookurl = input.Facebookurl;
        if (input.Googleurl != null) existingProfile.Googleurl = input.Googleurl;
        if (input.University != null) existingProfile.University = input.University;
        if (input.Title != null) existingProfile.Title = input.Title;
        if (input.Title2 != null) existingProfile.Title2 = input.Title2;
        if (input.Pronoun != null) existingProfile.Pronoun = input.Pronoun;
        if (input.Activepictureurl != null) existingProfile.Activepictureurl = input.Activepictureurl;
        existingProfile.Userid = input.Userid;
        if (input.Employeeid != null) existingProfile.Employeeid = input.Employeeid;
        if (input.Postalzip != null) existingProfile.Postalzip = input.Postalzip;
        if (input.Companyid != null) existingProfile.Companyid = input.Companyid;
        existingProfile.Buid = input.Buid;
        existingProfile.Managerid = input.Managerid;
        existingProfile.Regionid = input.Regionid;
        existingProfile.Branchid = input.Branchid;
        if (input.Defaultinstanceid != null) existingProfile.Defaultinstanceid = input.Defaultinstanceid;
        if (input.Defaultshardid != null) existingProfile.Defaultshardid = input.Defaultshardid;
        if (input.Useridasstring != null) existingProfile.Useridasstring = input.Useridasstring;

        await context.SaveChangesAsync();

        Enterpriseservices.ApiLogger.logapi(
            Enterpriseservices.Globals.ControllerAPIName,
            Enterpriseservices.Globals.ControllerAPINumber,
            "PUTWITHID", 1, "UpdateUserprofile", $"Updated ID: {input.Id}"
        );

        return TypedResults.Accepted($"Updated UserID: {input.Userid}");
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

