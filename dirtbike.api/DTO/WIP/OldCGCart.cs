/*using System;
using System.Collections.Generic;
using dirtbike.api.Models;

namespace dirtbike.api.DTOs
{
    // Root DTO for returning carts with nested items
    public class CGUserCartDto
    {
        public required int UserId { get; set; }
        public required string Username { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public List<CGCartDto> Carts { get; set; } = new();

        public static CGUserCartDto FromUser(User user, IEnumerable<Cart> carts, IEnumerable<Cartitem> items)
        {
            return new CGUserCartDto
            {
                UserId = user.Userid,
                Username = user.Username,
                FullName = user.Fullname,
                Email = user.Email,
                Carts = carts.Select(c =>
                {
                    var cartItems = items.Where(i => i.Cartid == c.Id).ToList();
                    return CGCartDto.FromCart(c, cartItems);
                }).ToList()
            };
        }
    }

    // DTO for Cart (transaction)
    public class CGCartDto
    {
        public required int Id { get; set; }
        public string? Uid { get; set; }
        public int? ParkId { get; set; }
        public string? ParkName { get; set; }
        public string? ItemType { get; set; }
        public string? ItemDescription { get; set; }
        public int? Quantity { get; set; }
        public double? UnitPrice { get; set; }
        public double? TotalPrice { get; set; }
        public DateOnly? DateAdded { get; set; }
        public int? IsCheckedOut { get; set; }
        public string? PaymentId { get; set; }
        public string? BookingInfo { get; set; }
        public double? TransactionTotal { get; set; }
        public DateTime? ResStart { get; set; }
        public DateTime? ResEnd { get; set; }
        public int? Adults { get; set; }
        public int? Children { get; set; }
        public int? TentSites { get; set; }
        public List<CGCartItemDto> Items { get; set; } = new();

        public static CGCartDto FromCart(Cart cart, IEnumerable<Cartitem> items)
        {
            return new CGCartDto
            {
                Id = cart.Id,
                Uid = cart.Uid,
                ParkId = cart.ParkId,
                ParkName = cart.Parkname,
                ItemType = cart.ItemType,
                ItemDescription = cart.ItemDescription,
                Quantity = cart.Quantity,
                UnitPrice = cart.UnitPrice,
                TotalPrice = cart.TotalPrice,
                DateAdded = cart.DateAdded,
                IsCheckedOut = cart.IsCheckedOut,
                PaymentId = cart.Paymentid,
                BookingInfo = cart.Bookinginfo,
                TransactionTotal = cart.Transactiontotal,
                ResStart = cart.ResStart,
                ResEnd = cart.ResEnd,
                Adults = cart.Adults,
                Children = cart.Children,
                TentSites = cart.Tentsites,
                Items = items.Select(i => CGCartItemDto.FromCartItem(i)).ToList()
            };
        }
    }

    // DTO for CartItem
    public class CGCartItemDto
    {
        public required int Id { get; set; }
        public string? ItemDescription { get; set; }
        public int? ItemQty { get; set; }
        public double? ItemExtendedPrice { get; set; }
        public double? ItemTotals { get; set; }
        public string? ProductId { get; set; }
        public string? ParkName { get; set; }

        public static CGCartItemDto FromCartItem(Cartitem item)
        {
            return new CGCartItemDto
            {
                Id = item.Id,
                ItemDescription = item.Itemdescription,
                ItemQty = item.Itemqty,
                ItemExtendedPrice = item.Itemextendedprice,
                ItemTotals = item.Itemtotals,
                ProductId = item.Productid,
                ParkName = item.Parkname
            };
        }
    }

    // DTO for inbound completed cart (POST)
    public class CGCompletedCartDto
    {
        public required int UserId { get; set; }
        public required string Uid { get; set; }
        public required double TransactionTotal { get; set; }
        public List<CGCompletedCartItemDto> Items { get; set; } = new();
    }

    public class CGCompletedCartItemDto
    {
        public required string ItemDescription { get; set; }
        public required int Quantity { get; set; }
        public required double UnitPrice { get; set; }
        public required double TotalPrice { get; set; }
        public string? ProductId { get; set; }
        public string? ParkName { get; set; }
    }
}*/
