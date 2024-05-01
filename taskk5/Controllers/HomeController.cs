using Faker;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using taskk5.Models;

namespace taskk5.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult GetUsers(string country, int seed, int page, int numberOfPersons)
        {
            Console.WriteLine(country);
            string[] data = country.Split(';');
            Console.WriteLine(data[0]);
            Console.WriteLine(data[1]);
            UserDataList users = new UserDataList(numberOfPersons, seed + page, data[0], data[1]);
            return Json(users.userDatas);
        }

        [HttpPost]
        public IActionResult SetErrors([FromBody] List<UserData> users, double error)
        {
            Error errors = new Error();
            int err = errors.GeneratenumError(error);
            users = errors.GenerateErrors(users, err);
            return Json(users);
        }

    }
}
