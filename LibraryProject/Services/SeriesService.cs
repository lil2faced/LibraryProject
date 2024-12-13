using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public class SeriesService
    {
        public static async Task<int> AddAsync(Series series, DatabaseContext _db)
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
        public static async Task<List<Series>> GetAllAsync(DatabaseContext _db)
        {
            return await _db.BookSeries.ToListAsync();
        }
        public static async Task<(int, Series?)> GetByIdAsync(int id, DatabaseContext _db)
        {
            var series = await _db.BookSeries.FindAsync(id);
            if (series == null)
            {
                return (1, series);
            }
            return (0, series);
        }
        public static async Task<int> DeleteByIdAsync(int id, DatabaseContext _db)
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
        public static async Task<int> UpdateByIDAsync(DatabaseContext _db, int id, Series ser)
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
