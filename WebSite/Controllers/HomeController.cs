using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BankSafe()
        {
            return View();
        }
        public IActionResult User()
        {
            return View();
        }
        public IActionResult BankAccount()
        {
            return View();
        }
        public IActionResult BankSafeTransactions()
        {
            return View();
        }
        public IActionResult BankSafeDocument()
        {
            return View();
        }
        public IActionResult login()
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
