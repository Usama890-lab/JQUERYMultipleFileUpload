using jFileUpload.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace jFileUpload.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private IWebHostEnvironment hostingEnvironment;
        public HomeController(IWebHostEnvironment environment)
        {
            hostingEnvironment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Upload(IFormFile files)
        {
            var resultList = new List<UploadFilesResult>();

            string path = Path.Combine(hostingEnvironment.WebRootPath, "uploads/" + files.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await files.CopyToAsync(stream);
            }

            UploadFilesResult uploadFiles = new UploadFilesResult();
            uploadFiles.name = files.FileName;
            uploadFiles.size = files.Length;
            uploadFiles.type = "image/jpeg";
            uploadFiles.url = "/uploads/" + files.FileName;
            uploadFiles.deleteUrl = "/Home/Delete?file=" + files.FileName;
            uploadFiles.thumbnailUrl = "/uploads/" + files.FileName;
            uploadFiles.deleteType = "GET";

            resultList.Add(uploadFiles);
            return Json(new { files = resultList });
        }

        public JsonResult Delete(string file)
        {
            System.IO.File.Delete(Path.Combine(hostingEnvironment.WebRootPath, "uploads/" + file));
            return Json("OK");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
