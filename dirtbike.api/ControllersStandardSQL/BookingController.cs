using Microsoft.AspNetCore.Mvc;
using dirtbike.api.Models;
using Enterpriseservices;   // <-- where BookingService10 lives

namespace Enterprise.Controllers;

public static class BookingEndpoints
{
    public static void MapBookingEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Booking").WithTags("Booking");

        // GET ALL
        group.MapGet("/", () =>
        {
            var svc = new BookingService10();
            return svc.GetAll10();
        })
        .WithName("GetAllBookings")
        .WithOpenApi();

        // GET BY ID
        group.MapGet("/{id}", (int id) =>
        {
            var svc = new BookingService10();
            return svc.GetById10(id);
        })
        .WithName("GetBookingById")
        .WithOpenApi();

        // CREATE
        group.MapPost("/", async (Booking input) =>
        {
            var svc = new BookingService10();
            return await svc.Create10(input);
        })
        .WithName("CreateBooking")
        .WithOpenApi();

        // UPDATE
        group.MapPut("/{id}", async (int id, Booking input) =>
        {
            var svc = new BookingService10();
            return await svc.Update10(id, input);
        })
        .WithName("UpdateBooking")
        .WithOpenApi();

        // DELETE
        group.MapDelete("/{id}", async (int id) =>
        {
            var svc = new BookingService10();
            return await svc.Delete10(id);
        })
        .WithName("DeleteBooking")
        .WithOpenApi();
    }
}
