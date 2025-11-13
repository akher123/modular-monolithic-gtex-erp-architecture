using Microsoft.AspNetCore.Mvc;

namespace Softcode.GTex.Web.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Hangfire.BackgroundJob.Enqueue<ApplicationService.IHangfireService>(ps => ps.StartJobQueueService());
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
