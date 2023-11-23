using Coffee.Data;
using Coffee.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Repositories
{
    public class NewsRepository
    {
        private ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<News>> GetNewsAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<List<News>> GetOnlyActiveNewsAsync()
        {
            return await _context.News.Where(x => x.IsActive).ToListAsync();
        }

        //Редактирование новостей
        public async Task<News> GetOneNewsAsync(int id)
        {
            return await _context.News.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<News> CreateNewsAsync(News news)
        {
            _context.News.Add(news);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

            return news;
        }

        //Редактирование новостей
        public async Task<News> UpdateNewsAsync(News news)
        {
            var item = await _context.News.Where(x => x.Id == news.Id).FirstOrDefaultAsync();

            item.Title = news.Title;
            item.Text = news.Text;
            item.Date = news.Date;
            item.IsActive = news.IsActive;

            await _context.SaveChangesAsync();

            return item;
        }

        //Удаление новостей
        public async Task DeleteNewsAsync(int id)
        {
            var item = await _context.News.Where(x => x.Id == id).FirstOrDefaultAsync();

            _context.News.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}