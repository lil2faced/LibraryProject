using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public class GenreService
    {
        public static async Task<int> AddAsync(Genre genre, DatabaseContext _db)
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
        public static async Task<List<Genre>> GetAllAsync(DatabaseContext _db)
        {
            return await _db.Genres.ToListAsync();
        }
        public static async Task<(int, Genre?)> GetByIdAsync(int id, DatabaseContext _db)
        {
            var genre = await _db.Genres.FindAsync(id);
            if (genre == null)
            {
                return (1, genre);
            }
            return (0, genre);
        }
        public static async Task<int> DeleteByIdAsync(int id, DatabaseContext _db)
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
        public static async Task<int> UpdateByIDAsync(DatabaseContext _db, int id, Genre gen)
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
