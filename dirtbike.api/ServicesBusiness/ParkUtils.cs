using System;
using System.Linq;
using System.Threading.Tasks;
using dirtbike.api.Data;
using dirtbike.api.Models;

namespace Enterpriseservices
{
    public class ParkUtils
    {
        public async Task UpdateVisitors(int somepark, int someadults, int somekids)
        {
            using (var context = new DirtbikeContext())
            {
                var calendar = context.Parks.FirstOrDefault(m => m.ParkId == somepark);

                if (calendar != null)
                {
                    calendar.Currentvisitors += (someadults + somekids);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task SetMaxVisitors(int somepark, int somemaxvisitors)
        {
            using (var context = new DirtbikeContext())
            {
                var park = context.Parks.FirstOrDefault(m => m.ParkId == somepark);

                if (park != null)
                {
                    park.Maxvisitors = somemaxvisitors;
                    await context.SaveChangesAsync();
                }
            }
        }

        public void CreateParkGuid()
        {
            using (var context = new DirtbikeContext())
            {
                var parks = context.Parks.ToList();

                foreach (var park in parks)
                {
                    // Convert GUID to string since your app expects string IDs
                    park.Id = Guid.NewGuid().ToString();
                }

                context.SaveChanges();
            }
        }
    }
}
