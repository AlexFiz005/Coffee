using Coffee.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffee.Repositories;

namespace Coffee.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private NewsRepository _newsRepository;

        public AdminController(NewsRepository newsRepository) 
        {
            _newsRepository = newsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            var listUsers = new List<string>();

            return View(listUsers);
        }

        public async Task<ActionResult> News()
        {
            var listNews = await _newsRepository.GetNewsAsync();

            return View(listNews);
        }
    }
}
