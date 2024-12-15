using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;
using LibraryProject.ControllerModels;
using AutoMapper;

namespace LibraryProject.Services
{
    public class GenreService : IGenreService
    {
        private readonly DatabaseContext _db;
        private readonly IMapper _mapper;
        public GenreService(DatabaseContext databaseContext, IMapper mapper)
        {
            _db = databaseContext;
            _mapper = mapper;
        }
        public async Task AddAsync(GenreModel genre, CancellationToken cancellationToken)
        {

            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (genre == null)
            {
                throw new ArgumentNullException();
            }
            var genre1 = await _db.Genres.Where(a => a.Name == genre.Name).FirstOrDefaultAsync();
            if (genre1 != null)
            {
                throw new Exception("Жанр не найден");
            }
            _db.Genres.Add(_mapper.Map<Genre>(genre));
            await _db.SaveChangesAsync();
        }
        public async Task<List<GenreModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            return await _db.Genres.Select(a => _mapper.Map<GenreModel>(a)).ToListAsync();
        }
        public async Task<GenreModel> GetByIdAsync(int? id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var genre = await _db.Genres.FindAsync(id);
            if (genre == null)
            {
                throw new Exception("Жанр не найден");
            }
            return _mapper.Map<GenreModel>(genre);
        }
        public async Task DeleteByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var genre = await _db.Genres.FindAsync(id);
            if (genre == null)
            {
                throw new Exception("Жанр не найден");
            }
            _db.Genres.Remove(genre);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateByIDAsync(int? id, GenreModel gen)
        {
            if (id == null || gen == null)
            {
                throw new ArgumentNullException();
            }
            var genre = await _db.Genres.FindAsync(id);
            if (genre == null)
            {
                throw new Exception("Жанр не найден");
            }
            genre.Name = gen.Name;
            _db.Genres.Update(genre);
            await _db.SaveChangesAsync();
        }
    }
}
