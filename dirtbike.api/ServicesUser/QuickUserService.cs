using dirtbike.api.DTOs;
using dirtbike.api.Models;
using dirtbike.api.Data;
using Microsoft.EntityFrameworkCore;
using Enterpriseservices;

public class QuickUserAddService
{
    public async Task<bool> CreateQuickUserAsync(QuickUserAdd dto)
    {
        try
        {
            using var context = new DirtbikeContext();
            using var transaction = await context.Database.BeginTransactionAsync();

            // 1. Create User
            var user = dto.ToUser();
            context.Users.Add(user);
            await context.SaveChangesAsync(); // generates user.UserId

            // 2. Create Userprofile
            var profile = dto.ToUserProfile(user.Userid);
            context.Userprofiles.Add(profile);

            // 3. Create UserPicture
            var picture = dto.ToUserPicture(user.Userid);
            context.UserPictures.Add(picture);

            // 4. Create CartMaster
            var cart = dto.ToCartMaster(user.Userid);
            context.CartMasters.Add(cart);

            // Save all dependent records
            await context.SaveChangesAsync();

            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            ApiLogger.logapi(
                Globals.ControllerAPIName,
                Globals.ControllerAPINumber,
                "QUICKADD", 0, "QuickUserAdd", $"Error: {ex.Message}");

            return false;
        }
    }
}

