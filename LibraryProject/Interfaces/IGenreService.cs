using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface IGenreService
    {
        Task AddAsync(Genre genre, CancellationToken cancellationToken);
        Task<List<Genre>> GetAllAsync(CancellationToken cancellationToken);
        Task<Genre> GetByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, Genre gen);
    }
}
