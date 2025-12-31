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
        group.MapGet("/user/{Useridstring}", (string Useridstring) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETUSERWITHID", 1, "Test", "Test"); 
                return context.Userprofiles.Where(m => m.Useridstring == Useridstring).ToList();
            }
        })
        .WithName("GetUserprofileByUserIdAsString")
        .WithOpenApi();


        //[HttpGet]
        group.MapGet("/userid/{Userid}", (int Userid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETUSERWITHID", 1, "Test", "Test");
                return context.Userprofiles.Where(m => m.Userid == Userid).ToList();
            }
        })
        .WithName("GetUserprofileByUserId")
        .WithOpenApi();


        group.MapPut("/profile/{userid}", async (int Userid, Userprofile input) =>
{
    await using var context = new DirtbikeContext();

    var existingProfile = await context.Userprofiles.FirstOrDefaultAsync(m => m.Userid == Userid);
    if (existingProfile == null)
    {
        return Results.NotFound();
    }

    // Update only if string is not null or empty
    if (!string.IsNullOrEmpty(input.Address1)) existingProfile.Address1 = input.Address1;
    if (!string.IsNullOrEmpty(input.Address2)) existingProfile.Address2 = input.Address2;
    if (!string.IsNullOrEmpty(input.City)) existingProfile.City = input.City;
    if (!string.IsNullOrEmpty(input.Stateregion)) existingProfile.Stateregion = input.Stateregion;
    if (!string.IsNullOrEmpty(input.Country)) existingProfile.Country = input.Country;
    if (!string.IsNullOrEmpty(input.Phone)) existingProfile.Phone = input.Phone;
    if (!string.IsNullOrEmpty(input.Cellphone)) existingProfile.Cellphone = input.Cellphone;
    if (!string.IsNullOrEmpty(input.Email)) existingProfile.Email = input.Email;
    if (!string.IsNullOrEmpty(input.Maritalstatus)) existingProfile.Maritalstatus = input.Maritalstatus;
    if (!string.IsNullOrEmpty(input.University1)) existingProfile.University1 = input.University1;
    if (!string.IsNullOrEmpty(input.University2)) existingProfile.University2 = input.University2;
    if (!string.IsNullOrEmpty(input.Linkedinurl)) existingProfile.Linkedinurl = input.Linkedinurl;
    if (!string.IsNullOrEmpty(input.Instagramurl)) existingProfile.Instagramurl = input.Instagramurl;
    if (!string.IsNullOrEmpty(input.Vimeourl)) existingProfile.Vimeourl = input.Vimeourl;
    if (!string.IsNullOrEmpty(input.Facebookurl)) existingProfile.Facebookurl = input.Facebookurl;
    if (!string.IsNullOrEmpty(input.Googleurl)) existingProfile.Googleurl = input.Googleurl;
    if (!string.IsNullOrEmpty(input.University)) existingProfile.University = input.University;
    if (!string.IsNullOrEmpty(input.Title)) existingProfile.Title = input.Title;
    if (!string.IsNullOrEmpty(input.Pronoun)) existingProfile.Pronoun = input.Pronoun;
    if (!string.IsNullOrEmpty(input.Title2)) existingProfile.Title2 = input.Title2;
    if (!string.IsNullOrEmpty(input.Activepictureurl)) existingProfile.Activepictureurl = input.Activepictureurl;
    if (!string.IsNullOrEmpty(input.Employeeid)) existingProfile.Employeeid = input.Employeeid;
    if (!string.IsNullOrEmpty(input.Postalzip)) existingProfile.Postalzip = input.Postalzip;
    if (!string.IsNullOrEmpty(input.Companyid)) existingProfile.Companyid = input.Companyid;
    if (!string.IsNullOrEmpty(input.Fullname)) existingProfile.Fullname = input.Fullname;
    if (!string.IsNullOrEmpty(input.Firstname)) existingProfile.Firstname = input.Firstname;
    if (!string.IsNullOrEmpty(input.Lastname)) existingProfile.Lastname = input.Lastname;
    if (!string.IsNullOrEmpty(input.Defaultinstanceid)) existingProfile.Defaultinstanceid = input.Defaultinstanceid;
    if (!string.IsNullOrEmpty(input.Defaultshardid)) existingProfile.Defaultshardid = input.Defaultshardid;
    if (!string.IsNullOrEmpty(input.Useridstring)) existingProfile.Useridstring = input.Useridstring;

    // Non-string fields (like ints) can be updated directly
    existingProfile.Sms = input.Sms;

    await context.SaveChangesAsync();

    Enterpriseservices.ApiLogger.logapi(
        Enterpriseservices.Globals.ControllerAPIName,
        Enterpriseservices.Globals.ControllerAPINumber,
        "PUTWITHID", 1, "UpdateUserprofile", $"Updated UserID: {Userid}"
    );

    return TypedResults.Accepted($"Updated UserID: {Userid}");
})
.WithName("UpdateUserprofileByUserid")
.WithOpenApi();

        group.MapPut("/profile/{useridasstring}", async (string Useridstring, Userprofile input) =>
        {
            await using var context = new DirtbikeContext();

            var existingProfile = await context.Userprofiles.FirstOrDefaultAsync(m => m.Useridstring == Useridstring);
            if (existingProfile == null)
            {
                return Results.NotFound();
            }

            // Update only if string is not null or empty
            if (!string.IsNullOrEmpty(input.Address1)) existingProfile.Address1 = input.Address1;
            if (!string.IsNullOrEmpty(input.Address2)) existingProfile.Address2 = input.Address2;
            if (!string.IsNullOrEmpty(input.City)) existingProfile.City = input.City;
            if (!string.IsNullOrEmpty(input.Stateregion)) existingProfile.Stateregion = input.Stateregion;
            if (!string.IsNullOrEmpty(input.Country)) existingProfile.Country = input.Country;
            if (!string.IsNullOrEmpty(input.Phone)) existingProfile.Phone = input.Phone;
            if (!string.IsNullOrEmpty(input.Cellphone)) existingProfile.Cellphone = input.Cellphone;
            if (!string.IsNullOrEmpty(input.Email)) existingProfile.Email = input.Email;
            if (!string.IsNullOrEmpty(input.Maritalstatus)) existingProfile.Maritalstatus = input.Maritalstatus;
            if (!string.IsNullOrEmpty(input.University1)) existingProfile.University1 = input.University1;
            if (!string.IsNullOrEmpty(input.University2)) existingProfile.University2 = input.University2;
            if (!string.IsNullOrEmpty(input.Linkedinurl)) existingProfile.Linkedinurl = input.Linkedinurl;
            if (!string.IsNullOrEmpty(input.Instagramurl)) existingProfile.Instagramurl = input.Instagramurl;
            if (!string.IsNullOrEmpty(input.Vimeourl)) existingProfile.Vimeourl = input.Vimeourl;
            if (!string.IsNullOrEmpty(input.Facebookurl)) existingProfile.Facebookurl = input.Facebookurl;
            if (!string.IsNullOrEmpty(input.Googleurl)) existingProfile.Googleurl = input.Googleurl;
            if (!string.IsNullOrEmpty(input.University)) existingProfile.University = input.University;
            if (!string.IsNullOrEmpty(input.Title)) existingProfile.Title = input.Title;
            if (!string.IsNullOrEmpty(input.Pronoun)) existingProfile.Pronoun = input.Pronoun;
            if (!string.IsNullOrEmpty(input.Title2)) existingProfile.Title2 = input.Title2;
            if (!string.IsNullOrEmpty(input.Activepictureurl)) existingProfile.Activepictureurl = input.Activepictureurl;
            if (!string.IsNullOrEmpty(input.Employeeid)) existingProfile.Employeeid = input.Employeeid;
            if (!string.IsNullOrEmpty(input.Postalzip)) existingProfile.Postalzip = input.Postalzip;
            if (!string.IsNullOrEmpty(input.Companyid)) existingProfile.Companyid = input.Companyid;
            if (!string.IsNullOrEmpty(input.Fullname)) existingProfile.Fullname = input.Fullname;
            if (!string.IsNullOrEmpty(input.Firstname)) existingProfile.Firstname = input.Firstname;
            if (!string.IsNullOrEmpty(input.Lastname)) existingProfile.Lastname = input.Lastname;
            if (!string.IsNullOrEmpty(input.Defaultinstanceid)) existingProfile.Defaultinstanceid = input.Defaultinstanceid;
            if (!string.IsNullOrEmpty(input.Defaultshardid)) existingProfile.Defaultshardid = input.Defaultshardid;
            if (!string.IsNullOrEmpty(input.Useridstring)) existingProfile.Useridstring = input.Useridstring;

            // Non-string fields (like ints) can be updated directly
            existingProfile.Sms = input.Sms;

            await context.SaveChangesAsync();

            Enterpriseservices.ApiLogger.logapi(
                Enterpriseservices.Globals.ControllerAPIName,
                Enterpriseservices.Globals.ControllerAPINumber,
                "PUTWITHID", 1, "UpdateUserprofile", $"Updated UserIDasstring: {Useridstring}"
            );

            return TypedResults.Accepted($"Updated UserIdstring: {Useridstring}");
        })
.WithName("UpdateUserprofileByUseridassting")
.WithOpenApi();

        group.MapPut("/{Id}", async (int Id, Userprofile input) =>
        {
            await using var context = new DirtbikeContext();

            var existingProfile = await context.Userprofiles.FirstOrDefaultAsync(m => m.Id == Id);
            if (existingProfile == null)
            {
                return Results.NotFound();
            }

            // Update only if string is not null or empty
            if (!string.IsNullOrEmpty(input.Address1)) existingProfile.Address1 = input.Address1;
            if (!string.IsNullOrEmpty(input.Address2)) existingProfile.Address2 = input.Address2;
            if (!string.IsNullOrEmpty(input.City)) existingProfile.City = input.City;
            if (!string.IsNullOrEmpty(input.Stateregion)) existingProfile.Stateregion = input.Stateregion;
            if (!string.IsNullOrEmpty(input.Country)) existingProfile.Country = input.Country;
            if (!string.IsNullOrEmpty(input.Phone)) existingProfile.Phone = input.Phone;
            if (!string.IsNullOrEmpty(input.Cellphone)) existingProfile.Cellphone = input.Cellphone;
            if (!string.IsNullOrEmpty(input.Email)) existingProfile.Email = input.Email;
            if (!string.IsNullOrEmpty(input.Maritalstatus)) existingProfile.Maritalstatus = input.Maritalstatus;
            if (!string.IsNullOrEmpty(input.University1)) existingProfile.University1 = input.University1;
            if (!string.IsNullOrEmpty(input.University2)) existingProfile.University2 = input.University2;
            if (!string.IsNullOrEmpty(input.Linkedinurl)) existingProfile.Linkedinurl = input.Linkedinurl;
            if (!string.IsNullOrEmpty(input.Instagramurl)) existingProfile.Instagramurl = input.Instagramurl;
            if (!string.IsNullOrEmpty(input.Vimeourl)) existingProfile.Vimeourl = input.Vimeourl;
            if (!string.IsNullOrEmpty(input.Facebookurl)) existingProfile.Facebookurl = input.Facebookurl;
            if (!string.IsNullOrEmpty(input.Googleurl)) existingProfile.Googleurl = input.Googleurl;
            if (!string.IsNullOrEmpty(input.University)) existingProfile.University = input.University;
            if (!string.IsNullOrEmpty(input.Title)) existingProfile.Title = input.Title;
            if (!string.IsNullOrEmpty(input.Pronoun)) existingProfile.Pronoun = input.Pronoun;
            if (!string.IsNullOrEmpty(input.Title2)) existingProfile.Title2 = input.Title2;
            if (!string.IsNullOrEmpty(input.Activepictureurl)) existingProfile.Activepictureurl = input.Activepictureurl;
            if (!string.IsNullOrEmpty(input.Employeeid)) existingProfile.Employeeid = input.Employeeid;
            if (!string.IsNullOrEmpty(input.Postalzip)) existingProfile.Postalzip = input.Postalzip;
            if (!string.IsNullOrEmpty(input.Companyid)) existingProfile.Companyid = input.Companyid;
            if (!string.IsNullOrEmpty(input.Fullname)) existingProfile.Fullname = input.Fullname;
            if (!string.IsNullOrEmpty(input.Firstname)) existingProfile.Firstname = input.Firstname;
            if (!string.IsNullOrEmpty(input.Lastname)) existingProfile.Lastname = input.Lastname;
            if (!string.IsNullOrEmpty(input.Defaultinstanceid)) existingProfile.Defaultinstanceid = input.Defaultinstanceid;
            if (!string.IsNullOrEmpty(input.Defaultshardid)) existingProfile.Defaultshardid = input.Defaultshardid;
            if (!string.IsNullOrEmpty(input.Useridstring)) existingProfile.Useridstring = input.Useridstring;

            // Non-string fields (like ints) can be updated directly
            existingProfile.Sms = input.Sms;

            await context.SaveChangesAsync();

            Enterpriseservices.ApiLogger.logapi(
                Enterpriseservices.Globals.ControllerAPIName,
                Enterpriseservices.Globals.ControllerAPINumber,
                "PUTWITHID", 1, "UpdateUserprofile", $"Updated Id: {Id}"
            );

            return TypedResults.Accepted($"Updated Id: {Id}");
        })
.WithName("UpdateUserprofileById")
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
    
    
    		group.MapPut("/picture/{id}", async (int id, ProfileDetail input) =>
            {
                using (var context = new DirtbikeContext())
                {               
                
                Userprofile[] someUserprofile = context.Userprofiles.Where(m => m.Userid == id).ToArray();
                context.Userprofiles.Attach(someUserprofile[0]);
                if (input.profileurl != null) 
                {
                someUserprofile[0].Activepictureurl = input.profileurl;
                }
                
                User[] someUser = context.Users.Where(m => m.Userid == id).ToArray();
                context.Users.Attach(someUser[0]);
                if (input.profileurl != null) 
                {
                someUser[0].Activepictureurl = input.profileurl;
                }

                    await context.SaveChangesAsync();

                    Enterpriseservices.ApiLogger.logapi(
                        Enterpriseservices.Globals.ControllerAPIName,
                        Enterpriseservices.Globals.ControllerAPINumber,
                        "PROFILEPICTUREUPDATE", 1, "UpdateUserprofile", $"Updated ID: {id}"
                    );
                }

                return TypedResults.Accepted($"Updated UserID Picture: {id}, {input.profileurl}");
            })
            .WithName("UpdatePicture")
            .WithOpenApi();
    }
}

