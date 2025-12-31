using dirtbike.api.Data;
using dirtbike.api.Models;
using Enterpriseservices;
using Microsoft.EntityFrameworkCore;

namespace Enterprise.Services;

public class PaymentService24 : IPaymentService24
{
    public IEnumerable<Payment> GetAll24()
    {
        using var context = new DirtbikeContext();
        ApiLogger.logapi("PaymentsAPI", "024", "GET", 1, "Test", "Test");
        return context.Payments.ToList();
    }

    public IEnumerable<Payment> GetById24(int id)
    {
        using var context = new DirtbikeContext();
        ApiLogger.logapi("PaymentsAPI", "024", "GETWITHID", 1, "Test", "Test");
        return context.Payments.Where(p => p.PaymentId == id).ToList();
    }

    public async Task<IResult> Create24(Payment input)
    {
        using var context = new DirtbikeContext();

        context.Payments.Add(input);
        await context.SaveChangesAsync();

        ApiLogger.logapi("PaymentsAPI", "024", "NEWRECORD", 1, "TEST", "TEST");

        return Results.Created($"/api/Payments/{input.PaymentId}", input);
    }

    public async Task<IResult> Update24(int id, Payment input)
    {
        using var context = new DirtbikeContext();

        var existing = await context.Payments.FirstOrDefaultAsync(p => p.PaymentId == id);
        if (existing == null)
            return Results.NotFound($"Payment {id} not found");

        // Update only allowed fields
        if (input.CardType != null)
            existing.CardType = input.CardType;

        await context.SaveChangesAsync();

        ApiLogger.logapi("PaymentsAPI", "024", "PUTWITHID", 1, "Test", "Test");

        return Results.Accepted($"Updated ID: {existing.PaymentId}");
    }

    public async Task<IResult> Delete24(int id)
    {
        using var context = new DirtbikeContext();

        var existing = await context.Payments.FirstOrDefaultAsync(p => p.PaymentId == id);
        if (existing == null)
            return Results.NotFound($"Payment {id} not found");

        context.Payments.Remove(existing);
        await context.SaveChangesAsync();

        ApiLogger.logapi("PaymentsAPI", "024", "DELETEWITHID", 1, "Test", "Test");

        return Results.Ok($"Deleted Payment {id}");
    }
}
