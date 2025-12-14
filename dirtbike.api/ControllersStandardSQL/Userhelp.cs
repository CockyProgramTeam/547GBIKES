using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http;
using dirtbike.api.Models;
using dirtbike.api.Data;
namespace Enterprise.Controllers;

public static class UserhelpEndpoints
{

    public static void MapUserhelpEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Userhelp").WithTags(nameof(Userhelp));

        //[HttpGet]
        group.MapGet("/", () =>
        {
            using (var context = new DirtbikeContext())
            {
                return context.Userhelps.ToList();
            }

        })
        .WithName("GetAllUserhelps")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                return context.Userhelps.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetUserhelpById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Userhelp input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Userhelp[] someUserhelp = context.Userhelps.Where(m => m.Id == id).ToArray();
                context.Userhelps.Attach(someUserhelp[0]);
                someUserhelp[0].Descr = input.Descr;
                someUserhelp[0].Emplid = input.Emplid;
                someUserhelp[0].Severity = input.Severity;
                someUserhelp[0].Userid = input.Userid;
                someUserhelp[0].Email = input.Email;
                someUserhelp[0].Fullname = input.Fullname;
                someUserhelp[0].Bestcontactnumber = input.Bestcontactnumber;
                someUserhelp[0].Replied = input.Replied;
                someUserhelp[0].Repliedmanagerid = input.Repliedmanagerid;
                someUserhelp[0].Repliedmanagerphone = input.Repliedmanagerphone;
                someUserhelp[0].Repliedmanageremail = input.Repliedmanageremail;
                someUserhelp[0].Ticketstatus = input.Ticketstatus;
                // Set today's date
                //someUserhelp[0].Ticketdate = DateTime.UtcNow;
                //someUserhelp[0].Responsedate = DateTime.UtcNow;

                await context.SaveChangesAsync();
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateUserhelp")
        .WithOpenApi();
    
    
     //[HttpPut]
        group.MapPut("/userid/{id}", async (int Userid, Userhelp input) =>
        {
            using (var context = new DirtbikeContext())
            {
                Userhelp[] someUserhelp = context.Userhelps.Where(m => m.Userid == Userid).ToArray();
                context.Userhelps.Attach(someUserhelp[0]);
                someUserhelp[0].Descr = input.Descr;
                someUserhelp[0].Emplid = input.Emplid;
                someUserhelp[0].Severity = input.Severity;
                someUserhelp[0].Userid = input.Userid;
                someUserhelp[0].Email = input.Email;
                someUserhelp[0].Fullname = input.Fullname;
                someUserhelp[0].Bestcontactnumber = input.Bestcontactnumber;
                someUserhelp[0].Replied = input.Replied;
                someUserhelp[0].Repliedmanagerid = input.Repliedmanagerid;
                someUserhelp[0].Repliedmanagerphone = input.Repliedmanagerphone;
                someUserhelp[0].Repliedmanageremail = input.Repliedmanageremail;

                // Set today's date
                //someUserhelp[0].Ticketdate = DateTime.UtcNow;
                //someUserhelp[0].Responsedate = DateTime.UtcNow;

                await context.SaveChangesAsync();
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateUserHelpByUserID")
        .WithOpenApi();
    
    
    

        group.MapPost("/", async (Userhelp input) =>
        {
            using (var context = new DirtbikeContext())
            {
                context.Userhelps.Add(input);
                await context.SaveChangesAsync();
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateUserhelp")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Userhelps.Add(std);
                Userhelp[] someUserhelps = context.Userhelps.Where(m => m.Id == id).ToArray();
                context.Userhelps.Attach(someUserhelps[0]);
                context.Userhelps.Remove(someUserhelps[0]);
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteUserhelp")
        .WithOpenApi();
    }
}

