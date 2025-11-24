using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using dirtbike.api.Models;
using dirtbike.api.Data;
using NuGet.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Services;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
namespace Enterprise.Controllers;
//using Services;


public enum Roles
{
    admin,
    registered,
    guest
}

public static class Auth
{
    private const string UsersFilePath = "Controllers/Auth/userList.json";
    private const string CredentialsFilePath = "Controllers/Auth/userCredential.json";

    //  Using HttpContext because the current JSON based setup requires reading and writing to JSON files in memory
    //  Since we are not using a database yet, we load entire JSON lists into memory and process them in the API
    //  Once the UserCred table is set up in a database, this will connect to DirtbikeContext for real time access
    //  JSON storage requires reading entire files before finding user data

    public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Auth").WithTags("Authentication");

        //  Login Route
        group.MapPost("/loginLocal", async (LoginRequest request, IConfiguration config) =>
        {
            var users = await LoadUsersFromJson();
            var user = users.FirstOrDefault(u => u.Username.ToLower() == request.Username.ToLower());

            if (user == null)
                return Results.BadRequest("User not found.");

            var credentials = await LoadCredentialsFromJson();
            var userCredential = credentials.FirstOrDefault(c => c.UserId == user.Id);
            if (userCredential == null)
                return Results.BadRequest("User credentials not found.");

            //  Verify password using bcrypt
            bool passwordMatches = BCrypt.Net.BCrypt.Verify(request.PlainPassword, userCredential.EncryptedPassword);
            if (!passwordMatches)
                return Results.BadRequest("Password mismatch.");

            var token = GenerateJwtToken(user, config);

            return Results.Ok(new
            {
                userId = user.Id,
                userEmail = user.Email,
                userFullName = user.Fullname,
                userUsername = user.Username,
                userRole = user.Role,
                token
            });
        })
        .WithName("loginUserLocal")
        .WithOpenApi();

        group.MapPost("/login", (LoginRequest request, IConfiguration config) =>
        {
            using (var context = new DirtbikeContext())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.Username.ToLower() == request.Username.ToLower());

                if (user == null)
                    return Results.BadRequest("User not found.");

                //  Use bcrypt to verify password
                bool passwordMatches = BCrypt.Net.BCrypt.Verify(request.PlainPassword, user.Hashedpassword);
                if (!passwordMatches)
                    return Results.BadRequest("Password mismatch.");

                var token = GenerateJwtToken(user, config);

                return Results.Ok(new
                {
                    userId = user.Id,
                    userFirstname = user.Firstname,
                    userLastname = user.Lastname,
                    userUsername = user.Username,
                    userEmail = user.Email,
                    IsEmployee = user.Employee,
                    EmployeeId = user.Employeeid,
                    userMicrosoftId = user.Microsoftid,
                    userNcrId = user.Ncrid,
                    userOracleId = user.Oracleid,
                    userAzureId = user.Azureid,
                    userJid = user.Jid,
                    userProfileUrl = user.Profileurl,
                    userRole = user.Role,
                    userFullName = user.Fullname,
                    userCompany = user.Companyid,
                    userBtn = user.Btn,
                    userIsCertified = user.Iscertified,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    token = token
                });

            }
        })
        .WithName("loginUser")
        .WithOpenApi();



        //  Signup Route
        group.MapPost("/signupLocal", async (SignupRequest request) =>
        {
            var users = await LoadUsersFromJson();

            if (users.Any(u => u.Email?.ToLower() == request.Email.ToLower()))
                return Results.BadRequest("Email is already registered.");

            if (users.Any(u => u.Username?.ToLower() == request.Username.ToLower()))
                return Results.BadRequest("Username is already in use.");

            var newUserId = users.Any() ? users.Max(c => c.Id) + 1 : 0;

            //  Hash the password with bcrypt
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.PlainPassword);

            var newUser = new User
            {
                Id = newUserId,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Username = request.Username,
                Email = request.Email,
                Fullname = $"{request.Firstname} {request.Lastname}",
                Role = Roles.registered.ToString(),
                Hashedpassword = hashedPassword,
                Passwordtype = 1,
                Profileurl = "",
                Employee = 0,
                Employeeid = "",
                Microsoftid = "",
                Ncrid = "",
                Oracleid = "",
                Azureid = "",
                Plainpassword = "", // Not really needed/maybe shouldn't put their plain password in the databse?
                Jid = null,
                Companyid = null,
                Resettoken = null,
                Resettokenexpiration = null
            };

            users.Add(newUser);
            await SaveUsersToJson(users);

            var credentials = await LoadCredentialsFromJson();
            var newCredId = credentials.Any() ? credentials.Max(c => c.Id) + 1 : 1;

            var newCredential = new UserCred
            {
                Id = newCredId,
                UserId = newUserId,
                EncryptedPassword = hashedPassword // Same as what's in User
            };

            credentials.Add(newCredential);
            await SaveCredentialsToJson(credentials);

            return Results.Created($"/api/auth/{newUser.Id}", new { message = "User registered successfully." });
        })
        .WithName("signupUserLocal")
        .WithOpenApi();

        group.MapPost("/signup", async (SignupRequest request) =>
        {
            using (var context = new DirtbikeContext())
            {
                // Check for existing email or username
                if (context.Users.Any(u => u.Email.ToLower() == request.Email.ToLower()))
                    return Results.BadRequest("Email is already registered.");

                if (context.Users.Any(u => u.Username.ToLower() == request.Username.ToLower()))
                    return Results.BadRequest("Username is already in use.");

                // Hash the password securely
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.PlainPassword);

                var newUser = new User
                {
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Username = request.Username,
                    Email = request.Email,
                    Fullname = $"{request.Firstname} {request.Lastname}",
                    Role = Roles.registered.ToString(),
                    Hashedpassword = hashedPassword,
                    Passwordtype = 1,
                    Profileurl = "",
                    Employee = 0,
                    Employeeid = "",
                    Microsoftid = "",
                    Ncrid = "",
                    Oracleid = "",
                    Azureid = "",
                    Plainpassword = "", // Leave blank or null
                    Jid = null,
                    Companyid = null,
                    Resettoken = null,
                    Resettokenexpiration = null
                };

                context.Users.Add(newUser);
                await context.SaveChangesAsync();

                return Results.Created($"/api/auth/{newUser.Id}", new { message = "User registered successfully." });
            }
        })
        .WithName("signupUser")
        .WithOpenApi();
    }
/*
        //  Forgot Password Route
        group.MapPost("/forgotPasswordLocal", async (ForgotPasswordRequest request, ServiceBusService serviceBusService, IConfiguration config) =>
        {
            var users = await LoadUsersFromJson();
            var user = users.FirstOrDefault(u => u.Email?.ToLower() == request.Email.ToLower());

            if (user == null)
                return Results.NotFound("User not found (JSON).");

            var resetToken = Guid.NewGuid().ToString();
            var resetTokenExpiration = DateTime.UtcNow.AddHours(1);

            user.Resettoken = resetToken;
            user.Resettokenexpiration = resetTokenExpiration;

            await SaveUsersToJson(users);

            var resetLink = $"{config["FrontendUrl"]}/ResetPassword?token={resetToken}";

            var message = new
            {
                email = user.Email,
                subject = "Reset Your Password (JSON)",
                body = $"Click the link to reset your password: {resetLink}"
            };

            //await serviceBusService.SendMessageAsync(JsonConvert.SerializeObject(message));

            return Results.Ok(new { message = "Reset link sent to your email (JSON)." });
        })
        .WithName("forgotPasswordLocal")
        .WithOpenApi();

        group.MapPost("/forgotPassword", async (ForgotPasswordRequest request, ServiceBusService serviceBusService, IConfiguration config) =>
        {
            using (var context = new DirtbikeContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());

                if (user == null)
                    return Results.NotFound("User not found (DB).");

                var resetToken = Guid.NewGuid().ToString();
                var resetTokenExpiration = DateTime.UtcNow.AddHours(1);

                user.Resettoken = resetToken;
                user.Resettokenexpiration = resetTokenExpiration;

                await context.SaveChangesAsync();

                var resetLink = $"{config["FrontendUrl"]}/ResetPassword?token={resetToken}";

                var message = new
                {
                    email = user.Email,
                    subject = "Reset Your Password (DB)",
                    body = $"Click the link to reset your password: {resetLink}"
                };

                //await serviceBusService.SendMessageAsync(JsonConvert.SerializeObject(message));

                return Results.Ok(new { message = "Reset link sent to your email (DB)." });
            }
        }).WithName("forgotPassword")
        .WithOpenApi();

        group.MapPost("/resetPasswordProfile", async (ResetPasswordRequestProfile request, HttpContext httpContext, IConfiguration config) =>
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
                return Results.Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = config["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = config["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                // Debug claims
                var claims = claimsPrincipal.Claims.Select(c => new { c.Type, c.Value }).ToList();
                // Console.WriteLine("All Claims:");
                // foreach (var claim in claims)
                // {
                //     Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                // }

                var username = claimsPrincipal.Claims.FirstOrDefault(c => 
                    c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                // Console.WriteLine($"Username from claim: {username}");

                using (var context = new DirtbikeContext())
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
                    
                    if (user == null)
                    {
                        // Console.WriteLine($"No user found for username: {username}");
                        return Results.BadRequest("User not found.");
                    }

                    // Verify current password
                    bool passwordMatches = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Hashedpassword);
                    if (!passwordMatches)
                        return Results.BadRequest("Current password is incorrect.");

                    // Update password
                    user.Hashedpassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

                    await context.SaveChangesAsync();

                    return Results.Ok(new { message = "Password successfully updated.\n\nNew password: " + request.NewPassword});
                }
            }
            catch (Exception ex)
            {
                return Results.Unauthorized();
            }
        })
        .WithName("resetPasswordProfile")
        .WithOpenApi();


        group.MapPost("/resetPasswordLocal", async (ResetPasswordRequest request) =>
        {
            if (request.ResetToken == null)
                return Results.BadRequest("Token is null, user sholdn't be on this page.");

            var users = await LoadUsersFromJson();
            var user = users.FirstOrDefault(u =>
                u.Resettoken == request.ResetToken &&
                u.Resettokenexpiration > DateTime.UtcNow);

            if (user == null)
                return Results.BadRequest("Invalid or expired token.");

            user.Hashedpassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.Resettoken = null;
            user.Resettokenexpiration = null;

            await SaveUsersToJson(users);

            return Results.Ok(new { message = "Password successfully reset (JSON)." });
        })
        .WithName("resetPasswordLocal")
        .WithOpenApi();

        group.MapPost("/resetPassword", async (ResetPasswordRequest request) =>
        {
            if (request.ResetToken == null)
                return Results.BadRequest("Token is null, user shouldn't be on this page.");

            using (var context = new DirtbikeContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(u =>
                    u.Resettoken == request.ResetToken &&
                    u.Resettokenexpiration > DateTime.UtcNow);

                if (user == null)
                    return Results.BadRequest("Invalid or expired token.");

                user.Hashedpassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                user.Resettoken = null;
                user.Resettokenexpiration = null;

                await context.SaveChangesAsync();

                return Results.Ok(new { message = "Password successfully reset (DB)." });
            }
        })
        .WithName("resetPassword")
        .WithOpenApi();

    }
*/

    // File Handling Methods
    private static async Task<List<User>> LoadUsersFromJson()
    {
        if (!File.Exists(UsersFilePath)) return new List<User>();
        var jsonData = await File.ReadAllTextAsync(UsersFilePath);
        return JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();
    }

    private static async Task SaveUsersToJson(List<User> users)
    {
        var jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
        await File.WriteAllTextAsync(UsersFilePath, jsonData);
    }

    private static async Task<List<UserCred>> LoadCredentialsFromJson()
    {
        if (!File.Exists(CredentialsFilePath)) return new List<UserCred>();
        var jsonData = await File.ReadAllTextAsync(CredentialsFilePath);
        return JsonConvert.DeserializeObject<List<UserCred>>(jsonData) ?? new List<UserCred>();
    }

    private static async Task SaveCredentialsToJson(List<UserCred> credentials)
    {
        var jsonData = JsonConvert.SerializeObject(credentials, Formatting.Indented);
        await File.WriteAllTextAsync(CredentialsFilePath, jsonData);
    }

    private static string GenerateJwtToken(User user, IConfiguration config)
    {
        //  In production should load from appSettings
        var jwtkey = config["Jwt:Key"];
        var issuer = config["Jwt:Issuer"];
        var audience = config["Jwt:Audience"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtkey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("role", user.Role) // Add user role claim
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2), // Token expiry
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}

public class LoginRequest { public string Username { get; set; } public string PlainPassword { get; set; } }
public class SignupRequest { public string Firstname { get; set; } public string Lastname { get; set; } public string Username { get; set; } public string Email { get; set; } public string PlainPassword { get; set; } }
public class ForgotPasswordRequest { public string Email { get; set; } }
public class ResetPasswordRequest { public string ResetToken { get; set; } public string NewPassword { get; set; } }
public class ResetPasswordRequestProfile { public string CurrentPassword { get; set; } public string NewPassword { get; set; } }






