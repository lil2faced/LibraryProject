using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;

namespace LibraryProject.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly DatabaseContext _db;
        public SeriesService(DatabaseContext databaseContext)
        {
            _db = databaseContext;
        }
        public async Task<int> AddAsync(Series series)
        {
            var series1 = await _db.BookSeries.Where(a => a.Name == series.Name).FirstOrDefaultAsync();
            if (series1 != null)
            {
                return 1;
            }
            _db.BookSeries.Add(series);
            await _db.SaveChangesAsync();
            return 0;
        }
        public async Task<List<Series>> GetAllAsync()
        {
            return await _db.BookSeries.ToListAsync();
        }
        public async Task<(int, Series?)> GetByIdAsync(int id)
        {
            var series = await _db.BookSeries.FindAsync(id);
            if (series == null)
            {
                return (1, series);
            }
            return (0, series);
        }
        public async Task<int> DeleteByIdAsync(int id)
        {
            var series = await _db.BookSeries.FindAsync(id);
            if (series == null)
            {
                return 1;
            }
            _db.BookSeries.Remove(series);
            await _db.SaveChangesAsync();
            return 0;
        }
        public async Task<int> UpdateByIDAsync(int id, Series ser)
        {
            var series = await _db.BookSeries.FindAsync(id);
            if (series == null)
            {
                return 1;
            }
            series.Name = ser.Name;
            series.Description = ser.Description;
            _db.BookSeries.Update(series);
            await _db.SaveChangesAsync();
            return 0;
        }
    }
}
