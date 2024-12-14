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
        public async Task AddAsync(Series series, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (series == null)
            {
                throw new ArgumentNullException();
            }
            var series1 = await _db.BookSeries.Where(a => a.Name == series.Name).FirstOrDefaultAsync();
            if (series1 != null)
            {
                throw new Exception("Такая серия уже существует");
            }
            _db.BookSeries.Add(series);
            await _db.SaveChangesAsync();
        }
        public async Task<List<Series>> GetAllAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            return await _db.BookSeries.ToListAsync();
        }
        public async Task<Series> GetByIdAsync(int? id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var series = await _db.BookSeries.FindAsync(id);
            if (series == null)
            {
                throw new Exception("Серия не найдена");
            }
            return series;
        }
        public async Task DeleteByIdAsync(int? id)
        {
            if(id == null) throw new ArgumentNullException();
            var series = await _db.BookSeries.FindAsync(id);
            if (series == null)
            {
                throw new Exception("Серия не найдена");
            }
            _db.BookSeries.Remove(series);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateByIDAsync(int? id, Series ser)
        {
            if (id == null || ser == null) throw new ArgumentNullException();
            var series = await _db.BookSeries.FindAsync(id);
            if (series == null)
            {
                throw new Exception("Серия не найдена");
            }
            series.Name = ser.Name;
            series.Description = ser.Description;
            _db.BookSeries.Update(series);
            await _db.SaveChangesAsync();
        }
    }
}
