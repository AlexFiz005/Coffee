using Coffee.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffee.Repositories;
using System.Security.Claims;
using Coffee.Repository;

namespace Coffee.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private NewsRepository _newsRepository;
        private DataRepository _dataRepository;

        public AdminController(NewsRepository newsRepository
            , DataRepository dataRepository)
        {
            _newsRepository = newsRepository;
            _dataRepository = dataRepository;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            bool isAdmin = User.IsInRole("Administrator");

            return View();
        }

        public async Task<ActionResult> Users()
        {
            var list = await _dataRepository.GetUsersAsync();

            return View(list);
        }

        [Route("/admin/users/block/{id}")]
        public async Task<ActionResult> BlockUsers(string id)
        {
            await _dataRepository.BlockUserAsync(id);

            return Redirect("/Admin/Users");
        }

        [Route("/admin/users/unblock/{id}")]
        public async Task<ActionResult> UnBlockUsers(string id)
        {
            await _dataRepository.UnBlockUserAsync(id);

            return Redirect("/Admin/Users");
        }

        public async Task<ActionResult> News()
        {
            var listNews = await _newsRepository.GetNewsAsync();

            return View(listNews);
        }

        [Route("/admin/news/createNews")]
        [HttpGet]
        public async Task<ActionResult> CreateNews()
        {

            return View();
        }

        [Route("/admin/news/createNews")]
        [HttpPost]
        public async Task<ActionResult> Create(News news)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId))
            {
                news.AuthorId = userId;

                news.Date = DateTime.SpecifyKind(news.Date, DateTimeKind.Utc);

                var result = await _newsRepository.CreateNewsAsync(news);
            }

            return Redirect("/Admin/News");
        }

        //Редактирование новостей
        [Route("/admin/news/edit/{id}")]
        [HttpGet]
        public async Task<ActionResult> EditNews(int id)
        {
            var news = await _newsRepository.GetOneNewsAsync(id);

            return View(news);
        }

        //Редактирование новостей
        [Route("/admin/news/edit/{id}")]
        [HttpPost]
        public async Task<ActionResult> Edit(News news)
        {

            news.Date = DateTime.SpecifyKind(news.Date, DateTimeKind.Utc);

            var result = await _newsRepository.UpdateNewsAsync(news);

            return Redirect("/Admin/News");
        }

        //Удаление новостей
        [Route("/admin/news/delete/{id}")]
        [HttpGet]
        public async Task<ActionResult> DeleteNews(int id)
        {
            await _newsRepository.DeleteNewsAsync(id);

            return Redirect("/Admin/News");
        }

    }
}

