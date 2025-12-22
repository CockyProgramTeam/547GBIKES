using Enterpriseservices; // <-- bring in FileIO
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using dirtbike.api.Models;
using dirtbike.api.Data;
using System.Reflection;
namespace EnterpriseControllers;


//THIS IS A FANCY GRID CONTROLLER.
//IT PROMPTS THE USER FOR AN ACTION WHICH IS AN INTEGER.
//IT RUNS FUNCTIONS ON THE CONSOLE, AND RETURNS DATA AS NECESSARY.
//OPTIONS 1,2,3,4 IMPACT PROCESSING OF PARKS FROM THE COMMAND LINE
//OPTIONS 5 DUMPS THE VALUE OF FILES IN THE QUEUE FOR PROCESSING.
//OPTIONS 6 REMOVES ZERO CARTS FOR A USER WHICH IS REQUIRED IN SOME SCREENS TO REVIEW PENDING CART ITEMS ON PARKS UI.
//OPTIONS 7/8 IMPACT THE PROCESSING OF PARK REVIEWS WHICH MAY NEED TO BE TRIGGERED FROM THE UI.



[ApiController]
[Route("[controller]")]
public class SystemConsoleController : ControllerBase
{
    private readonly string[] databaseNodes = { "10.144.0.100", "10.144.1.100", "10.144.2.100", "10.144.3.100", "10.144.4.100" };
    private readonly string[] webServerNodes = { "10.144.0.152", "10.144.1.151", "10.144.2.151", "10.144.3.151", "10.144.4.151" };
  
    private DirtbikeContext _context;

    [HttpGet("{option}")]
    public IActionResult GetSystemInfo(int option, int someint)
    {
        Enterpriseservices.Globals.ControllerAPIName = "ActionsController";
        Enterpriseservices.Globals.ControllerAPINumber = "001";

        switch (option)
        {
            case 1: return GetSchema();  //DUMPS the CURRENT SCHEMA TO /SQLDATA DIRECTORY
            case 2: return ProcessNCParks(); //Returns Sample Hub Data - Demo Only
            case 3: return ProcessVAParks(); //Returns Sample Hub Data - Demo Only
            case 4: return ProcessAllParks();   //Returns the Exact list of Data Files in the /DATA Directory
            case 5: return GetFileList(); //Runs all the I/O Functions and Imports all the Required Data.
            case 6: return RemoveZerocarts(someint); //Removes the ZeroCarts for a Userid which is an integer.
            case 7: return GetAvgs(someint); //Update Averages for a specific park
            case 8: return GetAllParkAvgs(); //Update Averages for all parks

            default:
                return BadRequest(new { Error = "Invalid option. Use 1 for hubs, 2 for history, 3 for branches, 4 for file list, 5 for full pipeline, 6 for 1-2-4, 7 for 1-2-5." });
        }
    }

    // -------------------------------
    // OPTION #4
    // -------------------------------
    private IActionResult GetFileList()
    {
        return Ok(1);
    }

    private IActionResult GetSchema()
    {
        
        Enterpriseservices.DirtbikeSchemaTools.SchemaDump();
        return Ok(1);

    }

    private IActionResult ProcessNCParks()
    {
        return Ok(1);
    }

    private IActionResult ProcessVAParks()
    {
        return Ok(1);
    }

    // -------------------------------
    // OPTION #5: Full Pipeline
    // -------------------------------
    private IActionResult ProcessAllParks()
    {
        return Ok(1);
    }

    // -------------------------------
    // OPTION #6: Allows for the Zeroing of Carts for a Specific User
    // -------------------------------
    private IActionResult RemoveZerocarts(int someint)
    {
        string somestring = someint.ToString();
        var service = new ZeroCartService(_context);  // use the injected DbContext
        string result = service.ZeroCartUpdate(somestring);

        return Ok(new { Message = $"ZeroCarts removed for User {someint}," + result });

    }

    // -------------------------------
    // OPTION #7: Updates the Avg Park Rating for a single park
    // -------------------------------
    private IActionResult GetAvgs(int someint)
    {
        int parkId = someint;
        var service = new ParkRatingService(_context);  // use the injected DbContext
        string result = service.UpdateAverageParkRating(parkId);

        return Ok(new { Message = $"Average rating updated for Park {parkId}," + result });
    }

// -------------------------------
    // OPTION #8: Updates the Avg Park Rating for the first 500 parks (Bigger than current DB)
    // -------------------------------

    private IActionResult GetAllParkAvgs()
    {

        var service = new ParkRatingService(_context);  // use the injected DbContext
        string result = service.UpdateAverageRatingsForFirst500();

        return Ok(new { Message = $"Average ratings updated for First 500Parks" + result });
    }

}

public class ControlBlock 
{ 
public string? somestring1 { get; set; } 
public string? somestring2 { get; set; } 
public required int i1 { get; set; } 
public required int i2 { get; set; }  
}



