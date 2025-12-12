using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using T4DATAPARKS;
using dirtbike.api.Models;
using dirtbike.api.Data;

namespace MENUSYSTEM137SQLITE
{
    public class Menu37
    {
        public static void DiagMenu()
        {
            using var ctx = new DirtbikeContext();

            int exit = 0;
            do
            {
                Console.WriteLine("\nSystem Utilities: Parks [V3.01] Installation and Maintenance.");
                Console.WriteLine("Parks Uses a React & HTML FrontEnd, C# RESTBackEnd.");
                Console.WriteLine("Please Enter Your Choice:");
                Console.WriteLine("1.Load Data into Parks");
                //Console.WriteLine("2.Load Park Reviews Seed Items");
                //Console.WriteLine("3.Load Cart Seed Data");
                //Console.WriteLine("4.Load Cart Seed Items");
                //Console.WriteLine("5. Load Cart Seed Users");
                Console.WriteLine("99.Exit");
                Console.Write("\nChoice: ");

                var input = Console.ReadLine();
                if (!int.TryParse(input, out var number)) number = 99;

                switch (number)
                {
                    case 99:
                        Console.WriteLine("\nYou Choose Option 99: (Exit). You Have Selected to Leave. Thank You.\n");
                        exit = 99;
                        break;

                    case 1:
                        Console.WriteLine("You Choose Option1: Load Data into Parks\n");
                        loadparks();
                        break;

                    case 2:
                        Console.WriteLine("You Choose Option: 2-Park Reviews\n");
                        break;

                    case 3:
                        Console.WriteLine("You Choose Option: 3-Cart\n");
                        break;

                    case 4:
                        Console.WriteLine("You Choose Option: 4-Cart Items\n");
                        break;

                    case 5:
                        Console.WriteLine("You Choose Option: 5-Users\n");
                        break;

                    default:
                        Console.WriteLine("\nYou Have Selected to Leave. Thank You.\n");
                        exit = 99;
                        break;
                }
            } while (exit != 99);
        }

        public static void loadparks()
        {
            using var ctx = new DirtbikeContext();

            if (!ctx.Parks.Any())
            {
                var parks = new List<Park>
                {
                    new Park
                    {
                        ParkId = 1001,
                        Name = "Banff National Park",
                        Address = "Banff, AB",
                        Phone = "403-762-1550",
                        Region = "Alberta",
                        TrailLengthMiles = 1000,
                        Difficulty = "Varies",
                        Description = "Canada's oldest national park established in 1885, featuring turquoise glacial lakes, towering mountain peaks, hot springs, and diverse wildlife. Home to iconic Lake Louise and Moraine Lake.",
                        DayPassPriceUsd = 10.5,
                        Longitude = (decimal)(-115.5708),
                        Latitude = (decimal)(51.4968),
                        Trailmapurl = "https://www.pc.gc.ca/en/pn-np/ab/banff/visit/cartes-maps",
                        Parklogourl = "https://ww2.547bikes.info/images/bannf.jpg",
                        Maxvisitors = 100,
                        Currentvisitors = 10,
                        Currentvisitorschildren = 5,
                        Currentvisitorsadults = 5,
                        Maxcampsites = 100,
                        State = "Alberta",
                        Pic1url = "https://ww2.547bikes.info/images/1/1.png",
                        Pic2url = "https://ww2.547bikes.info/images/1/2.png",
                        Pic3url = "https://ww2.547bikes.info/images/1/3.png",
                        Pic4url = "https://ww2.547bikes.info/images/1/4.png",
                        Pic5url = "https://ww2.547bikes.info/images/1/5.png",
                        Pic6url = "https://ww2.547bikes.info/images/1/6.png",
                        Pic7url = "https://ww2.547bikes.info/images/1/7.png",
                        Pic8url = "https://ww2.547bikes.info/images/1/8.png",
                        Pic9url = "https://ww2.547bikes.info/images/1/9.png",
                        Isnationalpark = "1",
                        Isstatepark = "0",
                        Hqbranchid = "BANF",
                        Mountainbikes = 1,
                        Camping = 1,
                        Rafting = 1,
                        Canoeing = 1,
                        Frisbee = 0,
                        Iscanadian = 0,
                        Ismexican = 0,
                        AverageRating = 4.2,
                        Id = "2D35C629-7BFE-5FFD-E3D8-E12EC7384716",
                        Currentcampsites = 10
                    },
                    new Park
                    {
                        ParkId = 1003,
                        Name = "Yoho National Park",
                        Address = "Field, BC",
                        Phone = "250-343-6783",
                        Region = "British Columbia",
                        TrailLengthMiles = 250,
                        Difficulty = "Varies",
                        Description = "Mountain paradise featuring Takakkaw Falls (one of Canada's highest waterfalls), Emerald Lake, Natural Bridge, and ancient fossil beds. 'Yoho' means 'awe' in Cree.",
                        DayPassPriceUsd = 10.5,
                        Longitude = (decimal)(-116.4982),
                        Latitude = (decimal)(51.3747),
                        Trailmapurl = "https://www.pc.gc.ca/en/pn-np/bc/yoho/visit/cartes-maps",
                        Parklogourl = "https://via.placeholder.com/200x200/228B22/FFFFFF?text=Yoho+NP",
                        Maxvisitors = 100,
                        Currentvisitors = 10,
                        Currentvisitorschildren = 5,
                        Currentvisitorsadults = 5,
                        Maxcampsites = 100,
                        State = "British Columbia",
                        Isnationalpark = "1",
                        Isstatepark = "0",
                        Hqbranchid = "YOHO",
                        Mountainbikes = 1,
                        Camping = 1,
                        Rafting = 1,
                        Canoeing = 1,
                        Frisbee = 0,
                        Iscanadian = 0,
                        Ismexican = 0,
                        AverageRating = 4.0,
                        Id = "354E97BB-213F-AD5B-EC60-6813243AAA58",
                        Currentcampsites = 10
                    },
                    new Park
                    {
                        ParkId = 1011,
                        Name = "Paris Mountain State Park",
                        Address = "2401 State Park Rd, Greenville, SC 29609",
                        Phone = "864-244-5565",
                        Region = "Upstate",
                        TrailLengthMiles = 5,
                        Difficulty = "Intermediate to Advanced",
                        Description = "A Great Place Named After Paris Hilton and that cool guy from the Illiad and the Odyssey.",
                        DayPassPriceUsd = 2,
                        Longitude = (decimal)(-82.41128),
                        Latitude = (decimal)(34.94158),
                        Trailmapurl = "https://southcarolinaparks.com/files/State%20Parks%20Files/Paris%20Mtn/PM-Trail%20Map3-9-2012.pdf",
                        Parklogourl = "https://ww2.547bikes.info/parismountain.png",
                        Maxvisitors = 100,
                        Currentvisitors = 10,
                        Currentvisitorschildren = 5,
                        Currentvisitorsadults = 5,
                        Maxcampsites = 100,
                        State = "SC",
                        Isnationalpark = "0",
                        Isstatepark = "1",
                        Hqbranchid = "33",
                        Mountainbikes = 1,
                        Camping = 1,
                        Rafting = 0,
                        Canoeing = 0,
                        AverageRating = 4.67,
                        Id = "FFCD0BEB-3A86-5E14-E6E0-859DAED4D975",
                        Currentcampsites = 10
                    },
                    new Park
                    {
                ParkId = 2001,
                Name = "Motobike Mayhem",
                Address = "Springwood, CO",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit...",
                Parklogourl = "https://placehold.co/600x400/334155/FFF?text=Motobike+Mayhem",
                Id = "92ed4740-12d9-4573-a8f1-c883ca216a00"
                // other fields left null/default
                    },
                new Park
                {
                ParkId = 2002,
                Name = "Crossbar Parkway",
                Address = "Springwood, CO",
                Description = "This park boasts extreme hills and fun drops for all thrill-seekers.",
                Parklogourl = "https://placehold.co/600x400/3321a5/FFF?text=Crossbar+Parkway",
                Id = "fc099512-96d4-497a-a42f-d7b3967abc03"
                // other fields left null/default
            }
                };

                ctx.Parks.AddRange(parks);
                ctx.SaveChanges();
                Console.WriteLine("Seeded Banff, Yoho, Paris Mountain parks.");
            }
            else
            {
                Console.WriteLine("Parks already loaded.");
            }
        }
    }
}
