using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dirtbike.api.Data;
using dirtbike.api.Models;

namespace somecontrollers.Controllers
{

   public static class FileGlobals
    {
        public const string GlobalProfilePath = "/greenvillesoftware/547bikes.info/www/profiles";
        public const string ReturnPathUrl = "https://ww2.547bikes.info/profiles";
    }


    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        private readonly string _localPath = "/greenvillesoftware/547bikes.info/www/profiles";

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<Dictionary<string, string>> UploadFile(IFormFile file, string? fileCategory = null)
        {
            var response = new Dictionary<string, string>();

            if (file == null || file.Length == 0)
            {
                response["message"] = "No file uploaded.";
                return response;
            }

            Directory.CreateDirectory(_localPath);
            var fileName = Path.GetFileName(file.FileName);
        	var savePath = Path.Combine(FileGlobals.GlobalProfilePath, fileName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            response["message"] = "File uploaded successfully";
            response["fileName"] = fileName;
            response["filePath"] = savePath;

            return response;
        }
    }

    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly string _localPath = "/greenvillesoftware/547bikes.info/www/profiles";

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }

        [HttpPost("uploadWithId")]
        public async Task<ActionResult<Dictionary<string, string>>> UploadFileWithId(IFormFile file, int id)
        {
            var response = new Dictionary<string, string>();

            if (file == null || file.Length == 0)
            {
                response["message"] = "No file uploaded.";
                return BadRequest(response);
            }

            try
            {
                Directory.CreateDirectory(_localPath);
                var fileName = Path.GetFileName(file.FileName);
                var savePath = Path.Combine(_localPath, fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Update Userprofile
                using (var context = new DirtbikeContext())
                {
                    var userProfile = context.Userprofiles.FirstOrDefault(m => m.Id == id);
                    if (userProfile != null)
                    {
                        userProfile.Activepictureurl = savePath;
                        context.Userprofiles.Update(userProfile);
                        await context.SaveChangesAsync();
                    }
                }

                // Update User
                using (var context = new DirtbikeContext())
                {
                    var user = context.Users.FirstOrDefault(m => m.Id == id);
                    if (user != null)
                    {
                        user.Profileurl = savePath;
                        user.Activepictureurl = savePath;
                        user.Activeprofileurl = savePath;
                        context.Users.Update(user);
                        await context.SaveChangesAsync();
                    }
                }

                response["message"] = "File uploaded successfully";
                response["fileName"] = fileName;
                response["filePath"] = savePath;
                response["userProfileUpdateMessage"] = "Change successful. Both User and UserProfile updated.";

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file");
                response["message"] = "Upload failed: " + ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
