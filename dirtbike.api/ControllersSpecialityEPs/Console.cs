using Enterpriseservices; // <-- bring in FileIO
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using dirtbike.api.Models;
using dirtbike.api.Data;
namespace EnterpriseControllers;


//THIS IS A FANCY GRID CONTROLLER.
//IT PROMPTS THE USER FOR AN ACTION WHICH IS AN INTEGER.
//IT RUNS FUNCTIONS ON THE CONSOLE, AND RETURNS DATA AS NECESSARY.
//ALL THE FILES IN THE DATA DIRECTORY CAN BE PROCESSED BY 4,5,6,7.
//OPTIONS 1,2,3 ARE HISTORICAL FROM OTHER GRID CONTROLLERS AND ONLY RETURN DIAGNOSTIC SAMPLE DATA WHICH IS A G/A NORMALIZATION.



[ApiController]
[Route("[controller]")]
public class SystemConsoleController : ControllerBase
{
    private readonly string[] databaseNodes = { "10.144.0.100", "10.144.1.100", "10.144.2.100", "10.144.3.100", "10.144.4.100" };
    private readonly string[] webServerNodes = { "10.144.0.152", "10.144.1.151", "10.144.2.151", "10.144.3.151", "10.144.4.151" };
    private readonly string[] gashubNodes = { "Tulsa", "Austin", "Dallas", "OklahomaCity", "Phoenix" };
    private DirtbikeContext _context;

    [HttpGet("{option}")]
    public IActionResult GetSystemInfo(int option, int someint)
    {
        Enterpriseservices.Globals.ControllerAPIName = "ActionsController";
        Enterpriseservices.Globals.ControllerAPINumber = "001";

        switch (option)
        {
            case 1: return ListGasHubs();  //Returns Sample Hub Data - Demo Only
            case 2: return GetPriceHistory(); //Returns Sample Hub Data - Demo Only
            case 3: return ProcessPrices(); //Returns Sample Hub Data - Demo Only
            case 4: return GetFileList();   //Returns the Exact list of Data Files in the /DATA Directory
            case 5: return RunFullPipeline(); //Runs all the I/O Functions and Imports all the Required Data.
            case 6: return RemoveZerocarts(someint); //Removes the ZeroCarts for a Userid which is an integer.
            case 7: return GetAvgs(someint);
            case 8: return GetAllParkAvgs();

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

    private IActionResult ListGasHubs()
    {
        return Ok(new { OurGasHubs = gashubNodes });
    }

    private IActionResult GetPriceHistory()
    {
        return Ok(new { OurGasHubs = gashubNodes });
    }

    private IActionResult ProcessPrices()
    {
        return Ok(new { Branches = gashubNodes });
    }

    // -------------------------------
    // OPTION #5: Full Pipeline
    // -------------------------------
    private IActionResult RunFullPipeline()
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





