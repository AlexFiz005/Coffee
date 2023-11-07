using Coffee.Models.Entities;
using Coffee.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Controllers
{
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;
        private NewsRepository _newsRepository;

        public NewsController(ILogger<NewsController> logger, NewsRepository newsRepository)
        {
            _logger = logger;
            _newsRepository = newsRepository;
        }

        public List<News> Curses { get; set; }

        public async Task<ActionResult> News()
        {
            var listNews = await _newsRepository.GetOnlyActiveNewsAsync();

            return View(listNews);
        }
    }
}

