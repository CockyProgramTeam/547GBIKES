using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CONNECTIONTEST
{
    internal class PINGME
    {
        static public void runpings()
        {
            int exit = 0;
            do
            {
            Console.WriteLine("\nSystem Utilities: Parks [V3.01] Installation and Maintenance.");
            Console.WriteLine("Parks Uses a React & HTML FrontEnd, C# RESTBackEnd.");
            Console.WriteLine("Please Enter Your Choice:");
            Console.WriteLine("1. Press One to Test Your SQLITE Server on PORT80:");
            Console.WriteLine("2. Press Two to Test Your SQLITE Server on PORT443:");
            Console.WriteLine("4. Test All Ports:");
            Console.WriteLine("99.Exit:");
            Console.WriteLine("Please Enter Your Choice(1,2,3,4,99):\n");
            string youranswer = Console.ReadLine();
            if (youranswer == "1")
            {
                pingsqlite80();
            }
            else if (youranswer == "2")
            {
                pingsqlite443();
            }
            else if (youranswer == "3")
            {
                pingsqlite443();
                pingsqlite80();
            }
            else if (youranswer == "4")
            {
                pingsqlite443();
                pingsqlite80();
            }    
            else if (youranswer == "99")
            {
                    Console.WriteLine("You Selected 99(Exit). Have a Nice Day");
                    exit = 99;
            }
                else 
            { 
                Console.WriteLine("Invalid Option. Leaving");
                exit = 99;
            }
            } while (exit != 99);
            
            Console.WriteLine("Press Any Key and Hit Return to Close Window");
            Console.ReadLine(); // Keep the console window open
        }
        static public void pingsqlite80()
        {
            Console.WriteLine("MYSQL: This program runs an MS-PS Connection Test to the Current DBMS Port And Returns the Results To The Screen");
            // Define the server name and port
            string server = "parks.547bikes.info";
            int port = 80;

            // Create the PowerShell command
            string command = $"Test-NetConnection -ComputerName {server} -Port {port}";

            // Set up the process start info
            ProcessStartInfo psi = new ProcessStartInfo("powershell", $"-Command \"{command}\"")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Start the process and get the output
            using (Process process = Process.Start(psi))
            {
                using (System.IO.StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
           
        }

        static public void pingsqlite443()
        {
                Console.WriteLine("SQLITE:This program runs an MS-PS Connection Test And Returns the Results To The Screen");
                // Define the server name and port
                string server = "parksapi.547bikes.info";
                int port = 443;

                // Create the PowerShell command
                string command = $"Test-NetConnection -ComputerName {server} -Port {port}";

                // Set up the process start info
                ProcessStartInfo psi = new ProcessStartInfo("powershell", $"-Command \"{command}\"")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Start the process and get the output
                using (Process process = Process.Start(psi))
                {
                    using (System.IO.StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine(result);
                    }
                }
            }
        }
}
