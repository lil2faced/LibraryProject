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
        public async Task<int> AddAsync(Genre genre)
        {
            var genre1 = await _db.Genres.Where(a => a.Name == genre.Name).FirstOrDefaultAsync();
            if (genre1 != null)
            {
                return 1;
            }
            _db.Genres.Add(genre);
            await _db.SaveChangesAsync();
            return 0;
        }
        public async Task<List<Genre>> GetAllAsync()
        {
            return await _db.Genres.ToListAsync();
        }
        public async Task<(int, Genre?)> GetByIdAsync(int id)
        {
            var genre = await _db.Genres.FindAsync(id);
            if (genre == null)
            {
                return (1, genre);
            }
            return (0, genre);
        }
        public async Task<int> DeleteByIdAsync(int id)
        {
            var genre = await _db.Genres.FindAsync(id);
            if (genre == null)
            {
                return 1;
            }
            _db.Genres.Remove(genre);
            await _db.SaveChangesAsync();
            return 0;
        }
        public async Task<int> UpdateByIDAsync(int id, Genre gen)
        {
            var genre = await _db.Genres.FindAsync(id);
            if (genre == null)
            {
                return 1;
            }
            genre.Name = gen.Name;
            _db.Genres.Update(genre);
            await _db.SaveChangesAsync();
            return 0;
        }
    }
}
