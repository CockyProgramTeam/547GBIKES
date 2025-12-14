using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
using dirtbike.api.Data;
using dirtbike.api.Models;
using Enterpriseservices; // <-- bring in SessionsLogger

namespace dirtbike.api.Services
{
    public class CGCartService
    {
        public CartProcessingResult CreateCart(CGCompletedCartDto dto)
        {
            using var context = new DirtbikeContext();
            var result = new CartProcessingResult();

            var user = context.Users.FirstOrDefault(u => u.Userid == dto.UserId);
            if (user == null)
            {
                result.OverallResult = "Fail";
                result.Items.Add(new ItemResult
                {
                    ItemNumber = 0,
                    Result = "FailUserNotFound",
                    Message = "User not found"
                });

                var dtoJson = JsonSerializer.Serialize(dto);
                SessionsLogger.SessionLog(
                    "UnknownUser",
                    dto.UserId,
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    "CartService",
                    $"Cart POST failed. Payload={dtoJson}");

                return result;
            }

            int itemIndex = 1;
            bool anyFailures = false;

            foreach (var itemDto in dto.Items)
            {
                // Lookup by GUID string
                var park = context.Parks.FirstOrDefault(p => p.Id == itemDto.Park.Id);
                if (park == null)
                {
                    result.Items.Add(new ItemResult
                    {
                        ItemNumber = itemIndex,
                        Result = "FailParkNotFound",
                        Message = $"Park {itemDto.Park.ParkName} not found"
                    });
                    anyFailures = true;
                }
                else
                {
                    int requestedVisitors = itemDto.NumAdults + itemDto.NumChildren;
                    int existingVisitors = context.Cartitems
                        .Where(ci => ci.Parkid == park.Id &&
                                     ci.ResStart <= itemDto.ResEnd &&
                                     ci.ResEnd >= itemDto.ResStart)
                        .Sum(ci => (ci.Adults ?? 0) + (ci.Children ?? 0));

                    if (existingVisitors + requestedVisitors > park.Maxvisitors)
                    {
                        result.Items.Add(new ItemResult
                        {
                            ItemNumber = itemIndex,
                            Result = "FailCapacity",
                            Message = $"Park {park.Name} exceeded capacity for {itemDto.ResStart:d} - {itemDto.ResEnd:d}"
                        });
                        anyFailures = true;
                    }
                    else
                    {
                        result.Items.Add(new ItemResult
                        {
                            ItemNumber = itemIndex,
                            Result = "Success",
                            Message = $"Park {park.Name} booking accepted"
                        });
                    }
                }
                itemIndex++;
            }

            if (anyFailures)
            {
                result.OverallResult = "Fail";
                var resultJson = JsonSerializer.Serialize(result);
                SessionsLogger.SessionLog(
                    user.Username,
                    user.Id,
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    "CartService",
                    resultJson);

                return result;
            }

            var cart = new Cart
            {
                Uid = dto.Uid,
                CartId = user.Id,
                Transactiontotal = dto.TransactionTotal,
                Paymentid = dto.PaymentId,
                IsCheckedOut = 1,
                DateAdded = DateOnly.FromDateTime(DateTime.UtcNow),
                Totalcartitems = dto.Items.Count,
                Multipleitems = dto.Items.Count > 1 ? 1 : 0,
                Parkname = dto.Items.Count == 1 ? dto.Items[0].Park.ParkName : null,
                //NEW LINES FOR RESERVATION PARENT.
                ResStart = dto.Items.Min(i => i.ResStart),
    			ResEnd = dto.Items.Max(i => i.ResEnd)
            };
            context.Carts.Add(cart);
            context.SaveChanges();

            foreach (var itemDto in dto.Items)
            {
                var park = context.Parks.FirstOrDefault(p => p.Id == itemDto.Park.Id);
                var item = new Cartitem
                {
                    Cartid = cart.Id,
                    Cartitemdate = DateTime.UtcNow,
                    Itemdescription = itemDto.Park.ParkName,
                    Itemqty = itemDto.NumAdults + itemDto.NumChildren,
                    Itemtotals = itemDto.TotalPrice,
                    Parkid = park?.Id, // numeric FK
                    Parkname = itemDto.Park.ParkName,
                    Adults = itemDto.NumAdults,
                    Children = itemDto.NumChildren,
                    NumDays = itemDto.NumDays,
                    ResStart = itemDto.ResStart,
                    ResEnd = itemDto.ResEnd,
                    CreatedDate = DateTime.UtcNow,
                    Userid = dto.UserId
                };
                context.Cartitems.Add(item);
            }
            context.SaveChanges();

            var booking = new Booking
            {
                Uid = dto.Uid,
                Cartid = cart.Id.ToString(), // safe conversion
                TransactionId = dto.PaymentId,
                QuantityAdults = dto.Items.Sum(i => i.NumAdults),
                QuantityChildren = dto.Items.Sum(i => i.NumChildren),
                TotalAmount = dto.TransactionTotal,
                Totalcartitems = dto.Items.Count,
                CartDetailsJson = JsonSerializer.Serialize(dto),
                Reservationstatus = "Confirmed",
                Reservationtype = "Audit",
                ResStart = dto.Items.Min(i => i.ResStart),
                ResEnd = dto.Items.Max(i => i.ResEnd),
                NumDays = dto.Items.Sum(i => i.NumDays)
            };
            context.Bookings.Add(booking);
            context.SaveChanges();

            var payment = new Payment
            {
                BookingId = booking.BookingId,
                Userid = user.Id,
                AmountPaid = dto.TransactionTotal,
                TransactionId = dto.PaymentId,
                PaymentDate = DateTime.UtcNow.ToString("o"),
                Transtype = "Audit"
            };
            context.Payments.Add(payment);
            context.SaveChanges();

            // Add SalesSession for completed transaction
            var salesSession = new SalesSession
            {
                Uid = dto.Uid,
                SessionStart = DateTime.UtcNow.ToString("o"),
                SessionEnd = DateTime.UtcNow.ToString("o"),
                CartId1 = cart.Id,
                CartPayload = JsonSerializer.Serialize(dto)
            };
            context.SalesSessions.Add(salesSession);
            context.SaveChanges();

            result.BookingId = booking.BookingId;
            result.OverallResult = "Success";

            var successJson = JsonSerializer.Serialize(result);
            SessionsLogger.SessionLog(
                user.Username,
                user.Id,
                DateTime.UtcNow,
                DateTime.UtcNow,
                "CartService",
                successJson);

            return result;
        }

        // === Additional CRUD methods ===

        public List<Cart> GetCartsByUserId(int userId)
        {
            using var context = new DirtbikeContext();
            return context.Carts.Where(c => c.CartId == userId).ToList();
        }

        public Cart? GetCartById(int cartId)
        {
            using var context = new DirtbikeContext();
            return context.Carts.FirstOrDefault(c => c.Id == cartId);
        }

        public bool UpdateCart(Cart updatedCart)
        {
            using var context = new DirtbikeContext();
            var existing = context.Carts.FirstOrDefault(c => c.Id == updatedCart.Id);
            if (existing == null) return false;

            context.Entry(existing).CurrentValues.SetValues(updatedCart);
            context.SaveChanges();
            return true;
        }

        public bool DeleteCart(int cartId)
        {
            using var context = new DirtbikeContext();
            var cart = context.Carts.FirstOrDefault(c => c.Id == cartId);
            if (cart == null) return false;

            context.Carts.Remove(cart);
            context.SaveChanges();
            return true;
        }
    }
}
