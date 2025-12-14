/*.using dirtbike.api.Data;
using dirtbike.api.Models;
using dirtbike.api.DTOs;

namespace dirtbike.api.Services
{
    public class CGCartService
    {
        // CREATE: Save a new cart with items
        public CGCartDto? CreateCart(CGCompletedCartDto dto)
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(
                    Enterpriseservices.Globals.ControllerAPIName,
                    Enterpriseservices.Globals.ControllerAPINumber,
                    "CGCREATECART", 1, "Create", $"Cart for User {dto.UserId}");

                var user = context.Users.FirstOrDefault(u => u.Userid == dto.UserId);
                if (user == null) return null;

                var cart = new Cart
                {
                    Uid = dto.Uid,
                    CartId = user.Id,
                    Transactiontotal = dto.TransactionTotal,
                    IsCheckedOut = 1,
                    DateAdded = DateOnly.FromDateTime(DateTime.UtcNow)
                };
                context.Carts.Add(cart);
                context.SaveChanges();

                foreach (var itemDto in dto.Items)
                {
                    var item = new Cartitem
                    {
                        Cartid = cart.Id,
                        Itemdescription = itemDto.ItemDescription,
                        Itemqty = itemDto.Quantity,
                        Itemextendedprice = itemDto.UnitPrice,
                        Itemtotals = itemDto.TotalPrice,
                        Productid = itemDto.ProductId,
                        Parkname = itemDto.ParkName,
                        CreatedDate = DateTime.UtcNow
                    };
                    context.Cartitems.Add(item);
                }
                context.SaveChanges();

                var items = context.Cartitems.Where(i => i.Cartid == cart.Id).ToList();
                return CGCartDto.FromCart(cart, items);
            }
        }

        // READ: Get all carts for a user
        public List<CGCartDto> GetCartsByUserId(int userid)
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(
                    Enterpriseservices.Globals.ControllerAPIName,
                    Enterpriseservices.Globals.ControllerAPINumber,
                    "CGGETCARTSBYUSER", 1, "Fetch", $"Carts for User {userid}");

                // Temporary override for testing
                userid = 10000;

                var user = context.Users.FirstOrDefault(u => u.Userid == userid);
                if (user == null) return new List<CGCartDto>();

                var carts = context.Carts.Where(c => c.CartId == user.Id).ToList();
                var cartIds = carts.Select(c => c.Id).ToList();
                var items = context.Cartitems.Where(i => i.Cartid != null && cartIds.Contains(i.Cartid.Value)).ToList();

                return carts.Select(c =>
                {
                    var cartItems = items.Where(i => i.Cartid == c.Id).ToList();
                    return CGCartDto.FromCart(c, cartItems);
                }).ToList();
            }
        }

        // READ: Get single cart by cartId
        public CGCartDto? GetCartById(int cartId)
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(
                    Enterpriseservices.Globals.ControllerAPIName,
                    Enterpriseservices.Globals.ControllerAPINumber,
                    "CGGETCARTBYID", 1, "Fetch", $"Cart {cartId}");

                var cart = context.Carts.FirstOrDefault(c => c.Id == cartId);
                if (cart == null) return null;

                var items = context.Cartitems.Where(i => i.Cartid == cart.Id).ToList();
                return CGCartDto.FromCart(cart, items);
            }
        }

        // UPDATE: Update cart details
        public bool UpdateCart(int cartId, CGCompletedCartDto dto)
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(
                    Enterpriseservices.Globals.ControllerAPIName,
                    Enterpriseservices.Globals.ControllerAPINumber,
                    "CGUPDATECART", 1, "Update", $"Cart {cartId}");

                var cart = context.Carts.FirstOrDefault(c => c.Id == cartId);
                if (cart == null) return false;

                cart.Transactiontotal = dto.TransactionTotal;
                cart.IsCheckedOut = 1;
                context.SaveChanges();

                var existingItems = context.Cartitems.Where(i => i.Cartid == cart.Id).ToList();
                context.Cartitems.RemoveRange(existingItems);

                foreach (var itemDto in dto.Items)
                {
                    var item = new Cartitem
                    {
                        Cartid = cart.Id,
                        Itemdescription = itemDto.ItemDescription,
                        Itemqty = itemDto.Quantity,
                        Itemextendedprice = itemDto.UnitPrice,
                        Itemtotals = itemDto.TotalPrice,
                        Productid = itemDto.ProductId,
                        Parkname = itemDto.ParkName,
                        CreatedDate = DateTime.UtcNow
                    };
                    context.Cartitems.Add(item);
                }
                context.SaveChanges();
                return true;
            }
        }

        // DELETE: Remove a cart
        public bool DeleteCart(int cartId)
        {
            using (var context = new DirtbikeContext())
            {
                Enterpriseservices.ApiLogger.logapi(
                    Enterpriseservices.Globals.ControllerAPIName,
                    Enterpriseservices.Globals.ControllerAPINumber,
                    "CGDELETECART", 1, "Delete", $"Cart {cartId}");

                var cart = context.Carts.FirstOrDefault(c => c.Id == cartId);
                if (cart == null) return false;

                var items = context.Cartitems.Where(i => i.Cartid == cart.Id).ToList();
                context.Cartitems.RemoveRange(items);
                context.Carts.Remove(cart);
                context.SaveChanges();
                return true;
            }
        }
    }
}
*/
