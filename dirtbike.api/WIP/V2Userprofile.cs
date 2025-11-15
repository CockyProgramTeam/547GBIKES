/*using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using dirtbike.api.Models;
using dirtbike.api.Data;
using Enterpriseservices;

namespace Enterprise.Controllers
{
    public static class UserprofileEndpoints
    {
        public static void MapUserprofileEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Userprofile").WithTags(nameof(Userprofile));
            Enterpriseservices.Globals.ControllerAPIName = "UserprofileAPI";
            Enterpriseservices.Globals.ControllerAPINumber = "001";

            // GET all user profiles
            group.MapGet("/", () =>
            {
                using (var context = new DirtbikeContext())
                {
                    Enterpriseservices.ApiLogger.logapi(
                        Enterpriseservices.Globals.ControllerAPIName,
                        Enterpriseservices.Globals.ControllerAPINumber,
                        "GET", 1, "Test", "Test"
                    );
                    return context.Userprofiles.ToList();
                }
            })
            .WithName("GetAllUserprofiles")
            .WithOpenApi();

            // GET user profile by ID
            group.MapGet("/{id}", (int id) =>
            {
                using (var context = new DirtbikeContext())
                {
                    Enterpriseservices.ApiLogger.logapi(
                        Enterpriseservices.Globals.ControllerAPIName,
                        Enterpriseservices.Globals.ControllerAPINumber,
                        "GETWITHID", 1, "Test", "Test"
                    );
                    return context.Userprofiles.Where(m => m.Id == id).ToList();
                }
            })
            .WithName("GetUserprofileById")
            .WithOpenApi();

            // GET user profile by Userid string
            group.MapGet("/user/{Useridasstring}", (string Userid) =>
            {
                using (var context = new DirtbikeContext())
                {
                    Enterpriseservices.ApiLogger.logapi(
                        Enterpriseservices.Globals.ControllerAPIName,
                        Enterpriseservices.Globals.ControllerAPINumber,
                        "GETUSERWITHID", 1, "Test", "Test"
                    );
                    return context.Userprofiles.Where(m => m.Useridasstring == Userid).ToList();
                }
            })
            .WithName("GetUserprofileByUserId")
            .WithOpenApi();

            // PUT update user profile
            group.MapPut("/{id}", async (int id, Userprofile input) =>
            {
                using (var context = new DirtbikeContext())
                {
                    var existingProfile = context.Userprofiles.FirstOrDefault(m => m.Userid == id);
                    if (existingProfile == null) return TypedResults.NotFound();

                    // Update fields if not null
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

            // PUT update profile picture
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
                someUser[0].Profileurl = input.profileurl;
                someUser[0].Activepictureurl = input.profileurl;
                someUser[0].Activeprofileurl = input.profileurl;
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

        
        
            // POST create new user profile
            group.MapPost("/", async (Userprofile input) =>
            {
                using (var context = new DirtbikeContext())
                {
                    context.Userprofiles.Add(input);
                    await context.SaveChangesAsync();

                    Enterpriseservices.ApiLogger.logapi(
                        Enterpriseservices.Globals.ControllerAPIName,
                        Enterpriseservices.Globals.ControllerAPINumber,
                        "NEWRECORD", 1, "CreateUserprofile", $"Created ID: {input.Id}"
                    );

                    return TypedResults.Created("Created ID:" + input.Id);
                }
            })
            .WithName("CreateUserprofile")
            .WithOpenApi();

            // DELETE user profile by ID
            group.MapDelete("/{id}", async (int id, Userprofile input) =>
            {
                using (var context = new DirtbikeContext())
                {
                    var profile = context.Userprofiles.FirstOrDefault(m => m.Id == input.id);
                    if (profile == null) return TypedResults.NotFound();

                    context.Userprofiles.Remove(profile);

                    Enterpriseservices.ApiLogger.logapi(
                        Enterpriseservices.Globals.ControllerAPIName,
                        Enterpriseservices.Globals.ControllerAPINumber,
                        "DELETEWITHID", 1, "DeleteUserprofile", $"Deleted ID: {id}"
                    );

                    await context.SaveChangesAsync();
                    return TypedResults.Ok($"Deleted ID: {id}");
                }
            })
            .WithName("DeleteUserprofile")
            .WithOpenApi();

   
}

}}*/
