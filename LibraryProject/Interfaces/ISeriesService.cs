using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface ISeriesService
    {
        Task AddAsync(Series series, CancellationToken cancellationToken);
        Task<List<Series>> GetAllAsync(CancellationToken cancellationToken);
        Task<Series> GetByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, Series ser);
    }
}
