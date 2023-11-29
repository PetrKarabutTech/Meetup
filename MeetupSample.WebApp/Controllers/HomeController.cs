using Microsoft.AspNetCore.Mvc;

namespace MeetupSample.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var response =  File("index.html", "text/html");
            return response;
        }
    }
}
