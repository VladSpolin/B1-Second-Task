using Microsoft.AspNetCore.Mvc;
using SecondTask.BusinessLogic.Interfaces;
using SecondTask.Models;
using System.Diagnostics;

namespace SecondTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileService _fileService;
        private readonly IAccountingService _accountingService;
        private readonly Parser.Parser _parser;
        IWebHostEnvironment _appEnvironment;

        public HomeController(IWebHostEnvironment appEnvironment, ILogger<HomeController> logger, 
            IFileService fileService, IAccountingService accountingService, Parser.Parser parser)
        {
            _appEnvironment = appEnvironment;
            _logger = logger;
            _fileService = fileService;
            _accountingService = accountingService;
            _parser = parser;
        }


        public IActionResult Index()
        {
            return View(_fileService.GetFiles()); //returns list of files
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult Upload(IFormFile uploadedFile)
        {

            if (uploadedFile != null)
            {
                var data = _parser.Parse(uploadedFile);
                _accountingService.ImportRange(data); // imports data to db
                _fileService.Add(new Model.Models.File { Name = uploadedFile.FileName, Data = data });
            }


            return RedirectToAction("Index");
        }
        
        public IActionResult Export()
        {
            return _parser.GenerateFile(_fileService.GetFiles()); //download file
        }


    }
}