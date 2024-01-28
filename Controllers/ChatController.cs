using Microsoft.AspNetCore.Mvc;

namespace Forum_Management_System.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Chat/Index.cshtml");
        }
    }
}
