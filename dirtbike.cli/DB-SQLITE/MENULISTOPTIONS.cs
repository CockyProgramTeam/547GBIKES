using System;
using System.Collections.Generic;
using System.Linq;
using T4DATAPARKS;

namespace MENUSYSTEM33
{
    public class Menu33
    {
        // Driver code 
        public static void DiagMenu()
        {
            var OurLists2 = new T4LISTSPARKS();

            int exit = 0;
            do
            {
                Console.WriteLine("\nSystem Utilities: Parks[V3.01] Installation and Maintenance.");
                Console.WriteLine("Items #1-5 Show Our Default Data To Be Loaded Into the System At Installation Time.");
                Console.WriteLine("Please Enter Your Choice:");
                Console.WriteLine("1. Parks");
                Console.WriteLine("2. Users");
                Console.WriteLine("3. Products");
                Console.WriteLine("4. Employees");
                Console.WriteLine("5. Regions");
                Console.WriteLine("99. Exit");
                Console.Write("\nChoice: ");

                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int number)) number = 99;

                switch (number)
                {
                    case 99:
                        Console.WriteLine("\nYou chose Exit. Thank you.\n");
                        exit = 99;
                        break;

                    case 1:
                        Console.WriteLine("You chose Option 1: Parks\n");
                        foreach (var p in OurLists2.ParksList)
                        {
                            Console.WriteLine($"{p.Id} {p.Manager} {p.StoreCode} {p.State}");
                        }
                        Console.WriteLine();
                        break;

                    case 2:
                        Console.WriteLine("You chose Option 2: Users\n");
                        Console.WriteLine(string.Join("\n", OurLists2.Users));
                        Console.WriteLine();
                        break;

                    case 3:
                        Console.WriteLine("You chose Option 3: Products\n");
                        foreach (var prod in OurLists2.Products)
                        {
                            Console.WriteLine($"{prod.ProductID}: {prod.ProductName} - ${prod.Price}");
                        }
                        Console.WriteLine();
                        break;

                    case 4:
                        Console.WriteLine("You chose Option 4: Employees\n");
                        Console.WriteLine(string.Join("\n", OurLists2.Employees));
                        Console.WriteLine();
                        break;

                    case 5:
                        Console.WriteLine("You chose Option 5: Regions\n");
                        Console.WriteLine(string.Join("\n", OurLists2.Regions));
                        Console.WriteLine();
                        break;

                    default:
                        Console.WriteLine("\nInvalid choice. Exiting.\n");
                        exit = 99;
                        break;
                }
            } while (exit != 99);
        }
    }
}
