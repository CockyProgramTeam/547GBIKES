using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using static System.Net.WebRequestMethods;
namespace somecontrollers.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using dirtbike.api.Data;
using dirtbike.api.Models;
using Enterpriseservices;


    [ApiController]
    [Route("[controller]")]
    public class AzureFileOpsController : ControllerBase
    {
        private readonly ILogger<AzureFileOpsController> _logger;

        public AzureFileOpsController(ILogger<AzureFileOpsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "AzureGetFileOps")]
        public IEnumerable<AzureFileMenu> Get()
        {
            Random rnd = new Random();
            int dice = rnd.Next(1000, 10000000);
            string somePath = new string(dice + "profilepicture.png");
            string somexconstring1 = new string("DefaultEndpointsProtocol=https;AccountName=590team1storage;AccountKey=Z1iryneyXo7RKOlBJNJFenF4zMxXrCyDLyCWaOjhCZa5DMsrvSnaUUzZT2RljrI/92bnCSA8xd/E+AStSTk9iQ==;EndpointSuffix=core.windows.net");
            return Enumerable.Range(1, 1).Select(index => new AzureFileMenu
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Filename = index + somePath,
                FilePath = "https://590team1storage.blob.core.windows.net/projectimages",
                SecurityToken = "Z1iryneyXo7RKOlBJNJFenF4zMxXrCyDLyCWaOjhCZa5DMsrvSnaUUzZT2RljrI/92bnCSA8xd/E+AStSTk9iQ==",
                BucketToken = "sp=r&st=2025-04-08T21:03:25Z&se=2025-04-09T05:03:25Z&spr=https&sv=2024-11-04&sr=c&sig=KCLqxiUzhhv7HYB80oRVnjXoFEX8WaIRdfWOUbhF4Lc%3D",
                BucketFullPath = "https://590team1storage.blob.core.windows.net/projectimages?sp=r&st=2025-04-08T21:03:25Z&se=2025-04-09T05:03:25Z&spr=https&sv=2024-11-04&sr=c&sig=KCLqxiUzhhv7HYB80oRVnjXoFEX8WaIRdfWOUbhF4Lc%3D",
                AzureBlobConnectionString = "DefaultEndpointsProtocol = https; AccountName = 590team1storage; AccountKey = Z1iryneyXo7RKOlBJNJFenF4zMxXrCyDLyCWaOjhCZa5DMsrvSnaUUzZT2RljrI / 92bnCSA8xd / E + AStSTk9iQ ==; EndpointSuffix = core.windows.net"
            })
            .ToArray();
        }
public class AzureFileMenu
{
    public DateOnly Date { get; set; }
    public required string Filename { get; set; }
    public required string FilePath { get; set; }
    public string? SecurityToken { get; set; }
    public required string BucketToken { get; set; }
    public required string BucketFullPath { get; set; }
    public required string AzureBlobConnectionString { get; set; }
}

    }
