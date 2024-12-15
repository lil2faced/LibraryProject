using LibraryProject.ControllerModels;

namespace LibraryProject.Interfaces
{
    public interface ISeriesService
    {
        Task AddAsync(SeriesModel series, CancellationToken cancellationToken);
        Task<List<SeriesModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<SeriesModel> GetByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, SeriesModel ser);
    }
}
