using dirtbike.api.Data;
using dirtbike.api.Models;
using Enterpriseservices;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace Enterpriseservices;

public class BookingService10 : IBookingService10
{
    public IEnumerable<Booking> GetAll10()
    {
        using var context = new DirtbikeContext();
        return context.Bookings.ToList();
    }

    public IEnumerable<Booking> GetById10(int id)
    {
        using var context = new DirtbikeContext();
        return context.Bookings.Where(b => b.BookingId == id).ToList();
    }

    public async Task<IResult> Create10(Booking input)
    {
        using var context = new DirtbikeContext();

        context.Bookings.Add(input);
        await context.SaveChangesAsync();

        ApiLogger.logapi("BookingAPI", "001", "NEWRECORD", 1, "TEST", "TEST");

        await SendEmailNotification(input);

        return Results.Created($"/api/Booking/{input.BookingId}", input);
    }

    public async Task<IResult> Update10(int id, Booking input)
    {
        using var context = new DirtbikeContext();

        var existing = await context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
        if (existing == null)
            return Results.NotFound($"Booking {id} not found");

        context.Entry(existing).CurrentValues.SetValues(input);
        await context.SaveChangesAsync();

        ApiLogger.logapi("BookingAPI", "001", "UPDATE", 1, "TEST", "TEST");

        return Results.Ok(existing);
    }

    public async Task<IResult> Delete10(int id)
    {
        using var context = new DirtbikeContext();

        var existing = await context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
        if (existing == null)
            return Results.NotFound($"Booking {id} not found");

        context.Bookings.Remove(existing);
        await context.SaveChangesAsync();

        ApiLogger.logapi("BookingAPI", "001", "DELETE", 1, "TEST", "TEST");

        return Results.Ok($"Deleted booking {id}");
    }

    // -----------------------------
    // INTERNAL HELPERS
    // -----------------------------

    private async Task SendEmailNotification(Booking input)
    {
        var notifier = new EmailNotifiers();

        string emailToUse = input.Emailnoticeaddress ?? "";

        if (!IsValidEmail(emailToUse))
        {
            emailToUse = "stritzj@email.sc.edu";
            ApiLogger.logapi("BookingAPI", "001", "EMAIL FALLBACK", 1, "TEST", "TEST");
        }

        string msg =
            $"547Bikes Reservation Created for Park {input.ParkName} + {input.TransactionId} + {DateTime.Today:MM/dd/yyyy}";

        try
        {
            await notifier.gmailsendnotificationasync(input.Userid ?? 0, emailToUse, msg);
        }
        catch
        {
            ApiLogger.logapi("BookingAPI", "001", "EMAIL SEND FAILED", 1, "TEST", "TEST");
        }
    }

    private bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        try
        {
            var _ = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
