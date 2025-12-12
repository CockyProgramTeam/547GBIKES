using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Query;
using MySql.Data.MySqlClient;
using T4DATAPARKS;
using T4DBSQLITE;
using T4SQLITEINSTALLER;
using PARKAPITESTER;
using Google.Protobuf.Collections;

namespace MENUSYSTEM35
{
    public class Menu35
    {

        // Declare 3 variables - id, age and name 
        int id;
        string storecode;
        string manager;
        string state;

       
        // Driver code 
        static public void DiagMenu()
        {
            int exit = 0;
            int number = 0;
            do
            {
                Console.WriteLine("\nSystem Utilities: Parks [V3.01] Installation and Maintenance.");
                Console.WriteLine("Parks Uses a React & HTML FrontEnd, C# RESTBackEnd.");
                Console.WriteLine("Please Enter Your Choice:");
                Console.WriteLine("9.Diagnostics Processes, and SQL.Utilities");
                Console.WriteLine("10.Installation Processes, and SQL.Utilities");
                Console.WriteLine("99.Exit:");
                Console.WriteLine("Please Enter Your Choice(9,99(Exit):\n");
                string somestring = "1";
                char myChar = '1';
                somestring = Console.ReadLine();
                //myChar = somestring[0];
                number = Convert.ToInt32(somestring);
                //string upperstring = somestring.ToUpper();
                if (number == 99)
                {
                    Console.WriteLine("\nYou Choose Option 99: (Exit). You Have Selected to Leave. Thank You.");
                    Console.WriteLine("\n\n");
                    exit = 99;
                }
                else if (number == 9)
                {
                    Console.WriteLine("You Choose Option: 9-UsersBySQL - QUERY DATA\n");
                    Console.WriteLine("Enter S for SQLITE,  E to Exit to Previous Menu");
                    do
                    {
                        somestring = Console.ReadLine();
                        if (somestring[0] == 'O')
                        {
                            /*//T4SQLITE.connectiontest();  //DumpUsers
                            //T4SQLITE.connectiontest1(); //DumpEmployees
                            //T4SQLITE.connectiontest2(); //DumpMovies
                            //T4SQLITE.connectiontest3(); //DumpRatings
                            //T4SQLITE.connectiontest4(); //DumpGenres
                            //T4SQLITE.connectiontest5(); //DumpStores
                            //T4SQLITE.connectiontest6(); //DumpRegions                                               
                            //T4SQLITE.showtables();*/
                            exit = 101;
                        }
                        else if (somestring[0] == 'S')
                        {
                                   // Call the API function and dump results
                            var parks = ParkApiClient.GetFirst10ParksAsync().Result;
                            Console.WriteLine("parkId | parkName | address");
                            Console.WriteLine("-----------------------------------");
                            foreach (var park in parks)
                            {
                            Console.WriteLine($"{park.ParkId} | {park.name} | {park.address}");
                            }
                            exit = 101;
                        }
                        else if (somestring[0] == 'E')
                        {
                            exit = 101;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Selection. Please Try Again or E to Exit to Previous Menu");
                        }
                    } while (exit != 101);
                    //Console.WriteLine(String.Join("\n", users));
                    //Console.WriteLine("\n");
                    exit = 9;
                }
                else
                {
                    Console.WriteLine("\nYou Have Selected to Leave. Thank You.");
                    Console.WriteLine("\n\n");
                    exit = 99;
                }
                somestring = null;
            } while (exit != 99);


        }
    }
}
