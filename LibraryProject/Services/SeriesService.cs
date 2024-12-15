using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;
using LibraryProject.ControllerModels;
using AutoMapper;

namespace LibraryProject.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly DatabaseContext _db;
        private readonly IMapper _mapper;
        public SeriesService(DatabaseContext databaseContext, IMapper mapper)
        {
            _mapper = mapper;
            _db = databaseContext;
        }
        public async Task AddAsync(SeriesModel series, CancellationToken cancellationToken)
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
            _db.BookSeries.Add(_mapper.Map<Series>(series));
            await _db.SaveChangesAsync();
        }
        public async Task<List<SeriesModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            return await _db.BookSeries.Select(a => _mapper.Map<SeriesModel>(a)).ToListAsync();
        }
        public async Task<SeriesModel> GetByIdAsync(int? id, CancellationToken cancellationToken)
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
            return _mapper.Map<SeriesModel>(series);
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
        public async Task UpdateByIDAsync(int? id, SeriesModel ser)
        {
            if (id == null || ser == null) throw new ArgumentNullException();
            var series = await _db.BookSeries.FindAsync(id);
            if (series == null)
            {
                throw new Exception("Серия не найдена");
            }
            series.Name = ser.Name;
            series.Description = ser.Description;
            _db.BookSeries.Update(_mapper.Map<Series>(series));
            await _db.SaveChangesAsync();
        }
    }
}
