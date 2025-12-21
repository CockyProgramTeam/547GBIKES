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
using dirtbike.api.DTOs;
using Enterpriseservices;
using Microsoft.Extensions.WebEncoders.Testing;
namespace Enterprise.Controllers;


public static class UserEndpoints
{
    
    public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/User").WithTags(nameof(User));
        Enterpriseservices.Globals.ControllerAPIName = "UserAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Users.ToList();
            }
            
        })
        .WithName("GetAllUsers")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Users.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetUserById")
        .WithOpenApi();

        group.MapGet("/userid/{Uidstring}", (string userid) =>
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Users.Where(m => m.Uidstring == userid).ToList();
            }
        })
        .WithName("GetUserByUserId")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/password/{id}", async (int id, string someplainpassword) =>
        {
            using (var context = new DirtbikeContext())
            {
                User[] someUser = context.Users.Where(m => m.Userid == id).ToArray();
                context.Users.Attach(someUser[0]);
                if (someplainpassword != null) someUser[0].Plainpassword = someplainpassword;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + id);
            }
        })
        .WithName("UpdatePassword")
        .WithOpenApi();


    group.MapPut("/user/{id}", async (int id, User input) =>
{
    await using var context = new DirtbikeContext();

    var existingUser = await context.Users.FirstOrDefaultAsync(m => m.Userid == id);
    if (existingUser == null)
    {
        return Results.NotFound();
    }

    // Update only if string is not null or empty
    if (!string.IsNullOrEmpty(input.Firstname)) existingUser.Firstname = input.Firstname;
    if (!string.IsNullOrEmpty(input.Lastname)) existingUser.Lastname = input.Lastname;
    if (!string.IsNullOrEmpty(input.Username)) existingUser.Username = input.Username;
    if (!string.IsNullOrEmpty(input.Email)) existingUser.Email = input.Email;
    if (!string.IsNullOrEmpty(input.Employeeid)) existingUser.Employeeid = input.Employeeid;
    if (!string.IsNullOrEmpty(input.Microsoftid)) existingUser.Microsoftid = input.Microsoftid;
    if (!string.IsNullOrEmpty(input.Ncrid)) existingUser.Ncrid = input.Ncrid;
    if (!string.IsNullOrEmpty(input.Oracleid)) existingUser.Oracleid = input.Oracleid;
    if (!string.IsNullOrEmpty(input.Azureid)) existingUser.Azureid = input.Azureid;
    if (!string.IsNullOrEmpty(input.Plainpassword)) existingUser.Plainpassword = input.Plainpassword;
    if (!string.IsNullOrEmpty(input.Hashedpassword)) existingUser.Hashedpassword = input.Hashedpassword;
    if (!string.IsNullOrEmpty(input.Role)) existingUser.Role = input.Role;
    if (!string.IsNullOrEmpty(input.Fullname)) existingUser.Fullname = input.Fullname;
    if (!string.IsNullOrEmpty(input.Resettoken)) existingUser.Resettoken = input.Resettoken;
    if (!string.IsNullOrEmpty(input.Btn)) existingUser.Btn = input.Btn;
    if (!string.IsNullOrEmpty(input.Groupid1)) existingUser.Groupid1 = input.Groupid1;
    if (!string.IsNullOrEmpty(input.Groupid2)) existingUser.Groupid2 = input.Groupid2;
    if (!string.IsNullOrEmpty(input.Groupid3)) existingUser.Groupid3 = input.Groupid3;
    if (!string.IsNullOrEmpty(input.Groupid4)) existingUser.Groupid4 = input.Groupid4;
    if (!string.IsNullOrEmpty(input.Groupid5)) existingUser.Groupid5 = input.Groupid5;
    if (!string.IsNullOrEmpty(input.Accountstatus)) existingUser.Accountstatus = input.Accountstatus;
    if (!string.IsNullOrEmpty(input.Accountactiondate)) existingUser.Accountactiondate = input.Accountactiondate;
    if (!string.IsNullOrEmpty(input.Accountactiondescription)) existingUser.Accountactiondescription = input.Accountactiondescription;
    if (!string.IsNullOrEmpty(input.Usertwofactortype)) existingUser.Usertwofactortype = input.Usertwofactortype;
    if (!string.IsNullOrEmpty(input.Usertwofactorkeysmsdestination)) existingUser.Usertwofactorkeysmsdestination = input.Usertwofactorkeysmsdestination;
    if (!string.IsNullOrEmpty(input.Twofactorkeyemaildestination)) existingUser.Twofactorkeyemaildestination = input.Twofactorkeyemaildestination;
    if (!string.IsNullOrEmpty(input.Twofactorprovider)) existingUser.Twofactorprovider = input.Twofactorprovider;
    if (!string.IsNullOrEmpty(input.Twofactorprovidertoken)) existingUser.Twofactorprovidertoken = input.Twofactorprovidertoken;
    if (!string.IsNullOrEmpty(input.Twofactorproviderauthstring)) existingUser.Twofactorproviderauthstring = input.Twofactorproviderauthstring;
    if (!string.IsNullOrEmpty(input.Uidstring)) existingUser.Uidstring = input.Uidstring;
    if (!string.IsNullOrEmpty(input.Activepictureurl)) existingUser.Activepictureurl = input.Activepictureurl;

    // Non-string fields can be updated directly
    existingUser.Employee = input.Employee;
    existingUser.Passwordtype = input.Passwordtype;
    existingUser.Jid = input.Jid;
    existingUser.Iscertified = input.Iscertified;
    existingUser.Usertwofactorenabled = input.Usertwofactorenabled;

    await context.SaveChangesAsync();

    Enterpriseservices.ApiLogger.logapi(
        Enterpriseservices.Globals.ControllerAPIName,
        Enterpriseservices.Globals.ControllerAPINumber,
        "PUTWITHID", 1, "UpdateUser", $"Updated UserID: {id}"
    );

    return TypedResults.Accepted($"Updated UserID: {id}");
})
.WithName("UpdateUser")
.WithOpenApi();

 group.MapPost("/specialityuser/", async (User input) =>
{
    using (var context = new DirtbikeContext())
    {
        // STEP 1: Add new User record
        context.Users.Add(input);
        await context.SaveChangesAsync(); // input.Userid is now populated

        // STEP 2: Insert UserProfile
        var profile = new Userprofile
        {
            Userid = input.Userid,
            Firstname = input.Firstname,
            Lastname = input.Lastname,
            Email = input.Email,
           };
        context.Userprofiles.Add(profile);
        await context.SaveChangesAsync(); // profile.Id populated

        // STEP 3: Insert CartMaster
        var cartMaster = new CartMaster
        {
            UserId = input.Userid,
            CartsCount = 0,
            CartsCancelled = 0,
            CartsActive = 0,
            CartsActiveList = string.Empty,
            Loyaltyid = string.Empty,
            Loyaltyvendor = string.Empty
        };
        context.CartMasters.Add(cartMaster);
        await context.SaveChangesAsync(); // cartMaster.Id populated

        // STEP 4: Fetch the very last User from the database
        var lastUser = context.Users
                              .OrderBy(u => u.Userid)
                              .LastOrDefault();

        if (lastUser != null)
        {
            // STEP 5: Generate random UidString
            var rnd = new Random();
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string prefix = new string(Enumerable.Repeat(letters, 3)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());

            lastUser.Uidstring = prefix + lastUser.Userid.ToString();
            lastUser.Displayname = lastUser.Fullname;

            // STEP 6: Update User with indices
            lastUser.UserProfileIndex = profile.Id;
            lastUser.CartMasterIndex  = cartMaster.Id;
            profile.Userid = lastUser.Userid;
            cartMaster.UserId = lastUser.Userid;
            context.Users.Update(lastUser);
            await context.SaveChangesAsync();
        }

        Enterpriseservices.ApiLogger.logapi(
            Enterpriseservices.Globals.ControllerAPIName,
            Enterpriseservices.Globals.ControllerAPINumber,
            "NEWRECORD", 1, "TEST", "TEST"
        );
    }
})
.WithName("SpecialityCreateUserForFutureUseWithoutTriggers")
.WithOpenApi();

 group.MapPost("/", async (User input) =>
{
    using (var context = new DirtbikeContext())
    {
        // STEP 1: Add new User record
        context.Users.Add(input);
        await context.SaveChangesAsync(); // input.Userid is now populated
       
        Enterpriseservices.ApiLogger.logapi(
            Enterpriseservices.Globals.ControllerAPIName,
            Enterpriseservices.Globals.ControllerAPINumber,
            "NEWRECORD", 1, "TEST", "TEST"
        );
    }
})
.WithName("CreateUserWhenTriggersExistInSql")
.WithOpenApi();

 group.MapPost("/userfull", async (User input) =>
{
    using (var context = new DirtbikeContext())
    {
        // STEP 1: Add new User record
        context.Users.Add(input);
        await context.SaveChangesAsync(); // input.Id is now populated

        int newId = input.Id;
        input.Userid = newId;
        input.Uidstring = newId.ToString(); 
        // In complex systems Useridstring might be different, 
        // but here we’re aligning them for simplicity.

        // STEP 2: Create the UserProfile tied to this User
        var someprofile = new Userprofile
        {
            Id = newId,
            Userid = newId,
            Useridstring = newId.ToString()
        };
        context.Userprofiles.Add(someprofile);

        // STEP 3: Create the UserPicture tied to this User
        var newpicturerecord = new UserPicture
        {
            Id = newId,
            Userid = newId,
            Useridstring = newId.ToString(),
            Activepictureurl = null,   // set default or from input
            Somepicture = null         // set default or from input
        };
        context.UserPictures.Add(newpicturerecord);

     // STEP 4: Create the UserPicture tied to this User
        var newcartmaster = new CartMaster
        {
            Id = newId,
            UserId = newId,
            Useridstring = newId.ToString(),
        };
        context.CartMasters.Add(newcartmaster);


        // STEP 5: Save related records
        await context.SaveChangesAsync();

        // Optional logging
        Enterpriseservices.ApiLogger.logapi(
            Enterpriseservices.Globals.ControllerAPIName,
            Enterpriseservices.Globals.ControllerAPINumber,
            "NEWRECORD", 1, "TEST", "TEST"
        );

        return Results.Created($"/userfull/{newId}", input);
    }
})
.WithName("CreateUserFull")
.WithOpenApi();






        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new DirtbikeContext())
            {
                //context.Users.Add(std);
                User[] someUsers = context.Users.Where(m => m.Userid == id).ToArray();
                context.Users.Attach(someUsers[0]);
                context.Users.Remove(someUsers[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteUser")
        .WithOpenApi();
    

        //WE BUILT A NEW CONTROLLER TO A DTO WHICH SHOULD TAKE A BASIC FORM AND BUILD A WORKING PROFILE WITH MINIMAL FIELDS

    	group.MapPost("/quickadd", async ([FromBody] QuickUserAdd dto) =>
        {
    Console.WriteLine($"QuickUserAdd DTO received: Username={dto.Username}, Fullname={dto.Fullname}, Email={dto.Email}, Role={dto.Role}");

    string logPath = "/opt/ga/547bikes/logs/quickusers.log";
    string logEntry = $"[{DateTime.Now}] QuickUserAdd received: Username={dto.Username}, Fullname={dto.Fullname}, Email={dto.Email}, Role={dto.Role}{Environment.NewLine}";

    try
    {
        string? directoryPath = Path.GetDirectoryName(logPath);
        if (!string.IsNullOrWhiteSpace(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        File.AppendAllText(logPath, logEntry);
    }
    catch (Exception ex)
    {
        File.AppendAllText("/opt/ga/547bikes/logs/error.log", $"[{DateTime.Now}] Error: {ex.Message}{Environment.NewLine}");
    }

    using (var context = new DirtbikeContext())
    {
        var user = dto.ToUser();   // 👈 use the DTO’s mapper

        context.Users.Add(user);
        await context.SaveChangesAsync();

        Enterpriseservices.ApiLogger.logapi(
            Enterpriseservices.Globals.ControllerAPIName,
            Enterpriseservices.Globals.ControllerAPINumber,
            "QUICKADD", 1, "QuickUserAdd", "Created");

        return TypedResults.Created($"Created User Successfully");
    }
})
.WithName("QuickAddUser")
.WithOpenApi();

    
    
    
    }
}

