using LibraryProject.ControllerModels;

namespace LibraryProject.Interfaces
{
    public interface ISeriesService
    {
        Task AddAsync(SeriesDTO series, CancellationToken cancellationToken);
        Task<List<SeriesDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<SeriesDTO> GetByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, SeriesDTO ser);
    }
}
