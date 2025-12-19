using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Enterpriseservices;
using dirtbike.api.Models;
using dirtbike.api.Data;


namespace Enterprise.Controllers
{
    public static class CreditCardEndpoints
    {
        public static void MapCreditCardEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Card/Validate").WithTags(nameof(Card));
            Enterpriseservices.Globals.ControllerAPIName = "CreditCardAPI";
            Enterpriseservices.Globals.ControllerAPINumber = "002";

            // [HttpPost] Validate Credit Card
            group.MapPost("/", (CreditCardInput input) =>
            {
                bool isValid = CreditCardValidator.IsValidCardNumber(input.CardNumber);
                string cardType = CreditCardValidator.GetCardType(input.CardNumber);

                // Log the API call
                Enterpriseservices.ApiLogger.logapi(
                    Enterpriseservices.Globals.ControllerAPIName,
                    Enterpriseservices.Globals.ControllerAPINumber,
                    "VALIDATECARD",
                    1,
                    input.CardNumber,
                    isValid ? "VALID" : "INVALID"
                );

                // Return result
                return TypedResults.Ok(new
                {
                    CardNumber = input.CardNumber,
                    IsValid = isValid,
                    CardType = cardType
                });
            })
            .WithName("ValidateCreditCard")
            .WithOpenApi();
        }
    }

    // DTO for input
    public class CreditCardInput
    {
        public string CardNumber { get; set; }
    }
}
