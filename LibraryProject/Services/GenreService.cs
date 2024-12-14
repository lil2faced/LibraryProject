using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;

namespace LibraryProject.Services
{
    public class GenreService : IGenreService
    {
        private readonly DatabaseContext _db;
        public GenreService(DatabaseContext databaseContext)
        {
            _db = databaseContext;
        }
        public async Task AddAsync(Genre genre, CancellationToken cancellationToken)
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
            _db.Genres.Add(genre);
            await _db.SaveChangesAsync();
        }
        public async Task<List<Genre>> GetAllAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            return await _db.Genres.ToListAsync();
        }
        public async Task<Genre> GetByIdAsync(int? id, CancellationToken cancellationToken)
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
            return genre;
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
        public async Task UpdateByIDAsync(int? id, Genre gen)
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
