using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface IGenreService
    {
        Task<int> AddAsync(Genre genre);
        Task<List<Genre>> GetAllAsync();
        Task<(int, Genre?)> GetByIdAsync(int? id);
        Task<int> DeleteByIdAsync(int? id);
        Task<int> UpdateByIDAsync(int? id, Genre gen);
    }
}
