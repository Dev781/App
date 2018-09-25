using System.Diagnostics;
using WebApplication.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IViewRenderService _viewRenderService;
        private PdfTemplateModel TestModel { get; set; }
        public HomeController(IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
            TestModel = new PdfTemplateModel
            {
                Age = 45,
                Country = "United Kingdom",
                Name = "John Smith"
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> DownloadPdf([FromServices] INodeServices nodeServices)
        {
            var html = await _viewRenderService.RenderToStringAsync("Home/PdfTemplate", TestModel);
            var result = await nodeServices.InvokeAsync<byte[]>("./pdf", html);

            return File(result, "application/pdf", "myFile.pdf");
        }

        [HttpGet]
        public IActionResult PdfTemplate()
        {
            return View("~/Views/Home/PdfTemplate.cshtml", TestModel);
        }
    }
}