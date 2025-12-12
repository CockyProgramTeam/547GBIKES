using System;
using System.Collections.Generic;

namespace T4DATAPARKS
{
    public class T4LISTSPARKS
    {
        public class Park
        {
            public int Id { get; set; }
            public string? Address1 { get; set; }
            public string? Address2 { get; set; }
            public string? StoreCode { get; set; }
            public string? Manager { get; set; }
            public string? State { get; set; }
            public string? Longitude { get; set; }
            public string? Latitude { get; set; }
        }

        public class Product
        {
            public int ProductID { get; set; }
            public string? ProductName { get; set; }
            public decimal Price { get; set; }
        }

        // Match the names your other code expects
        public List<Park> ParksList = new List<Park>
        {
            new Park{ Id = 101, Manager = "Sravan", StoreCode = "Forrest33", State = "SC" },
            new Park{ Id = 102, Manager = "Deepu", StoreCode = "Chicago01", State = "IL" },
            new Park{ Id = 103, Manager = "Manoja", StoreCode = "Dallas01", State = "TX" },
            new Park{ Id = 104, Manager = "Sathwik", StoreCode = "Sacramento01", State = "CA" },
            new Park{ Id = 105, Manager = "Saran", StoreCode = "Seattle01", State = "WA" },
            new Park{ Id = 106, Manager = "Joe", StoreCode = "Smithfield01", State = "VA" },
            new Park{ Id = 107, Manager = "April", StoreCode = "Stohrs01", State = "VT" }
        };

        public List<Product> Products = new List<Product>
        {
            new Product { ProductID = 1, ProductName = "Sr. Ticket", Price = 5.99m },
            new Product { ProductID = 2, ProductName = "Child Ticket", Price = 4.99m },
            new Product { ProductID = 3, ProductName = "Adult Ticket", Price = 8.99m }
        };

        public List<string> Users = new List<string>
        {
            "john@test.com", "ryan@gmail.com", "brenan@microsoft.com",
            "scott@wsfs.com", "jackson@fort.com", "liam@taken.com"
        };

        public List<string> Ratings = new List<string> { "G", "PG", "R", "Adult", "History", "PG-13" };
        public List<string> Movies = new List<string> { "TopGun2", "BlueCrush", "IronMan2" };
        public List<string> Genres = new List<string> { "70s", "80s", "90s" };
        public List<string> Regions = new List<string> { "NE", "SE", "NC", "SC", "SW", "NW" };
        public List<string> Showtimes = new List<string> { "1:30", "3:30", "5:30", "7:30", "9:30" };
        public List<string> Employees = new List<string> { "John", "Hannah", "Lucy", "Bill" };
        public List<string> StoreCodes = new List<string> { "Forrest33", "Chicago01", "Dallas01", "Seattle01", "Sacramento01" };
    }
}
