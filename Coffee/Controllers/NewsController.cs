using Microsoft.AspNetCore.Mvc;

namespace Coffee.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult News()
        {
            return View();
        }
    }
}

