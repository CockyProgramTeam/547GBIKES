using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Query;
using MySql.Data.MySqlClient;
using T4DATA;
using T4DBMYSQL;
using T4DBMSSQL;
using T4DBPGSQL;
using T4DATA;
using T4MYSQLINSTALLER;
using T4MSSQLINSTALLER;
using T4PGSQLINSTALLER;
using static T4DATA.T4LISTS;
using Google.Protobuf.Collections;

namespace MENUSYSTEM33
{
    public class Menu33
    {

        public override string ToString()
        {
            return "LinqSelected:" + id + " " + manager + "        " + storecode + "," + state;
        }

        // Driver code 
        static public void DiagMenu()
        {
            T4LISTS OurLists2 = new T4LISTS();

            int exit = 0;
            int number = 0;
            do
            {
                Console.WriteLine("\nSystem Utilities: Parks[V3.01] Installation and Maintenance.");
                Console.WriteLine("Items #1-5 Show Our Default Data To Be Loaded Into the System At Installation Time.");
                Console.WriteLine("Please Enter Your Choice:");
                Console.WriteLine("1.Parks:");
                Console.WriteLine("2.Users:");
                Console.WriteLine("3.CartMasters:");
                Console.WriteLine("4.Employees:");
                Console.WriteLine("5.Regions:");
                Console.WriteLine("99.Exit:");
                Console.WriteLine("Please Enter Your Choice(1,2,3,4,5,99(Exit):\n");
                string somestring = "1";
                char myChar = '1';
                somestring = Console.ReadLine();
                //myChar = somestring[0];
                number = Convert.ToInt32(somestring);
                //string upperstring = somestring.ToUpper();
                if (number == 99)
                {
                    Console.WriteLine("\nYou Choose Option E: (Exit). You Have Selected to Leave. Thank You.");
                    Console.WriteLine("\n\n");
                    exit = 99;
                }
                else if (number == 1)
                {
                    Console.WriteLine("You Choose Option: 1-Parks\n");
                    Console.WriteLine(String.Join("\n", OurLists2.parks));
                    Console.WriteLine("\n\n");
                    exit = 1;
                }
                else if (number == 2)
                {
                    Console.WriteLine("You Choose Option: 2-Users\n");
                    Console.WriteLine(String.Join("\n", OurLists2.users));
                    Console.WriteLine("\n\n");
                    exit = 2;
                }
                else if (number == 3)
                {
                    Console.WriteLine("You Choose Option: 3-Ratings\n");
                    Console.WriteLine(String.Join("\n", OurLists2.cartmasters));
                    Console.WriteLine("\n\n");
                    exit = 3;
                }
                else if (number == 4)
                {
                    Console.WriteLine("You Choose Option: 4-Showtimes\n");
                    Console.WriteLine(String.Join("\n", OurLists2.employees));
                    Console.WriteLine("\n\n");
                    exit = 4;
                }
                else if (number == 5)
                {
                    Console.WriteLine("You Choose Option: 5-Regions\n");
                    Console.WriteLine(String.Join("\n", OurLists2.regions));
                    Console.WriteLine("\n\n");
                    exit = 5;
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
