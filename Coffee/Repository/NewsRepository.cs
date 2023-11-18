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

        public async Task<News> CreateNewsAsync(News news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();

            return news;
        }
    }
}