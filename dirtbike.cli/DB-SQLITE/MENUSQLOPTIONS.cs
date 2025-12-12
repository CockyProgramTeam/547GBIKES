using System;
using System.Diagnostics;
using T4SQLITEINSTALLER;

namespace MENUSYSTEM34
{
    public class Menu34
    {
        int id;
        string storecode;
        string manager;
        string state;

        public override string ToString()
        {
            return $"LinqSelected: {id} {manager} {storecode}, {state}";
        }

        // Driver code 
        public static void DiagMenu()
        {
            int exit = 0;
            int number = 0;
            do
            {
                Console.WriteLine("\nSystem Utilities: Parks [V3.01] Installation and Maintenance.");
                Console.WriteLine("Parks Uses a React & HTML FrontEnd, C# RESTBackEnd.");
                Console.WriteLine("Please Enter Your Choice:");
                Console.WriteLine("9.Diagnostics Processes, and SQL.Utilities");
                Console.WriteLine("10.Installation (S)QLITE");
                Console.WriteLine("11.Load Sample Data");
                Console.WriteLine("99.Exit:");
                Console.Write("\nChoice: ");

                string? somestring = Console.ReadLine();
                if (!int.TryParse(somestring, out number)) number = 99;

                if (number == 99)
                {
                    Console.WriteLine("\nYou chose Exit. Thank you.\n");
                    exit = 99;
                }
                else if (number == 10)
                {
                    Console.WriteLine("You chose Option 10: Installation\n");
                    Console.WriteLine("Enter S for SQLite3, E to Exit to Previous Menu");
                    do
                    {
                        somestring = Console.ReadLine();
                        if (string.IsNullOrEmpty(somestring)) continue;

                        char choice = char.ToUpperInvariant(somestring[0]);
                        if (choice == 'O')
                        {
                            // MYSQLINSTALLER.createmysqldb(); // if you implement MySQL installer
                            exit = 101;
                        }
                        else if (choice == 'S')
                        {
                            SQLITEINSTALLER.createsqlitedb();
                            exit = 101;
                        }
                        else if (choice == 'E')
                        {
                            exit = 101;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Selection. Please Try Again or E to Exit to Previous Menu");
                        }
                    } while (exit != 101);
                    exit = 10;
                }
                else
                {
                    Console.WriteLine("\nYou have selected to leave. Thank you.\n");
                    exit = 99;
                }
            } while (exit != 99);
        }
    }
}



