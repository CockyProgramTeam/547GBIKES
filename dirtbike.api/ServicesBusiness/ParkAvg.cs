using dirtbike.api.Data;
using dirtbike.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Enterpriseservices
{
    public class ParkRatingService
    {
        private readonly DirtbikeContext _context;

        public ParkRatingService(DirtbikeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Updates the AverageRating field for a given park based on its reviews.
        /// </summary>
        public string UpdateAverageParkRating(int parkId)
        {
            using (var context = new DirtbikeContext())
            {
                // Step 1: Count reviews for the park
                var reviewCount = context.ParkReviews
                                         .Count(r => r.ParkId == parkId);

                // Step 2: Calculate average if reviews exist, otherwise 0
                double averageRating = 0;
                if (reviewCount > 0)
                {
                    averageRating = context.ParkReviews
                                           .Where(r => r.ParkId == parkId)
                                           .Average(r => r.Stars);
                }

                // Step 3: Find the park
                var park = context.Parks
                                  .FirstOrDefault(p => p.ParkId == parkId);

                    if (park == null)
                    {
                        return "No such park.";
                    }
                

                // Step 4: Update AverageRating
                if (averageRating > 0.0)
                {
                    park.AverageRating = (float)averageRating;
                }
                else
                {
                    park.AverageRating = 0;
                }
                    // Step 5: Save changes
                    context.SaveChanges();
                string somestring = "Result" + (float)averageRating + "Total Records: " + reviewCount;
                return somestring;
            }
        }

        //UPDATE ALL AVERAGES.....

        public string UpdateAverageRatingsForFirst500()
        {
            using (var context = new DirtbikeContext())
            {
                int startId = 1;
                int endId = 500;
                int updatedCount = 0;

                for (int parkId = startId; parkId <= endId; parkId++)
                {
                    // Step 1: Count reviews for the park
                    var reviewCount = context.ParkReviews
                                             .Count(r => r.ParkId == parkId);

                    // Step 2: Calculate average if reviews exist, otherwise 0
                    double averageRating = 0;
                    if (reviewCount > 0)
                    {
                        averageRating = context.ParkReviews
                                               .Where(r => r.ParkId == parkId)
                                               .Average(r => r.Stars);
                    }

                    // Step 3: Find the park
                    var park = context.Parks
                                      .FirstOrDefault(p => p.ParkId == parkId);

                    if (park != null)
                    {
                        // Step 4: Update AverageRating
                        park.AverageRating = (float)averageRating;
                        updatedCount++;
                    }
                }

                // Step 5: Save changes once after loop
                context.SaveChanges();

                return $"Updated {updatedCount} parks (IDs {startId} to {endId}).";
            }
        }
    }

    }
