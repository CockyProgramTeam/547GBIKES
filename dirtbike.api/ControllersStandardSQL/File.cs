using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace Enterprise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileOpsController : ControllerBase
    {
        private readonly ILogger<FileOpsController> _logger;
        private readonly string _localProfilePath = "/home/wwwroot/profiles";

        public FileOpsController(ILogger<FileOpsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetFileOps")]
        public IEnumerable<FileMenu> Get()
        {
            var files = Directory.Exists(_localProfilePath)
                ? Directory.GetFiles(_localProfilePath)
                : Array.Empty<string>();

            var fileMenus = new List<FileMenu>();

            foreach (var filePath in files)
            {
                var fileName = Path.GetFileName(filePath);
                fileMenus.Add(new FileMenu
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Filename = fileName,
                    FilePath = filePath,
                    BucketFullPath = filePath,
                    SecurityToken = "local",
                    BucketToken = "local",
                    AzureBlobConnectionString = "local"
                });
            }

            return fileMenus;
        }
    }

    public class FileMenu
	{
    public DateOnly Date { get; set; }
    public string Filename { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string SecurityToken { get; set; } = string.Empty;
    public string BucketToken { get; set; } = string.Empty;
    public string BucketFullPath { get; set; } = string.Empty;
    public string AzureBlobConnectionString { get; set; } = string.Empty;
	}
}
