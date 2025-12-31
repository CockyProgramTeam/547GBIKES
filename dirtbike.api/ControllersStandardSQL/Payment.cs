using Enterprise.Services;
using dirtbike.api.Models;

namespace Enterprise.Controllers;

public static class PaymentsEndpoints
{
    public static void MapPaymentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Payments").WithTags(nameof(Payment));

        // GET ALL
        group.MapGet("/", () =>
        {
            var svc = new PaymentService24();
            return svc.GetAll24();
        })
        .WithName("GetAllPayments")
        .WithOpenApi();

        // GET BY ID
        group.MapGet("/{id}", (int id) =>
        {
            var svc = new PaymentService24();
            return svc.GetById24(id);
        })
        .WithName("GetPaymentById")
        .WithOpenApi();

        // CREATE
        group.MapPost("/", async (Payment input) =>
        {
            var svc = new PaymentService24();
            return await svc.Create24(input);
        })
        .WithName("CreatePayment")
        .WithOpenApi();

        // UPDATE
        group.MapPut("/{id}", async (int id, Payment input) =>
        {
            var svc = new PaymentService24();
            return await svc.Update24(id, input);
        })
        .WithName("UpdatePayment")
        .WithOpenApi();

        // DELETE
        group.MapDelete("/{id}", async (int id) =>
        {
            var svc = new PaymentService24();
            return await svc.Delete24(id);
        })
        .WithName("DeletePayment")
        .WithOpenApi();
    }
}
