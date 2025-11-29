using dirtbike.api.Data;
using dirtbike.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Enterpriseservices
{
    public class ZeroCartService
    {
        private readonly DirtbikeContext _context;

        public ZeroCartService(DirtbikeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Sets all carts for a user to closed (IsCheckedOut = 2)
        /// where Quantity is zero.
        /// </summary>
        public string ZeroCartUpdate(string uid)
        {
            // Get all carts for this user where Quantity is 0 or null
            var zeroCarts = _context.Carts
                .Where(c => c.Uid == uid && (c.Quantity ?? 0) == 0)
                .ToList();

            if (zeroCarts.Count == 0)
            {
                return "No zero‑quantity carts found.";
            }

            // Update each cart
            foreach (var cart in zeroCarts)
            {
                cart.IsCheckedOut = 2;   // 2 = closed
            }

            _context.SaveChanges();

            return $"Closed {zeroCarts.Count} zero‑item carts for user {uid}.";
        }
    }
}

    

    
