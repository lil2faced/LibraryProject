using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface ISeriesService
    {
        Task<int> AddAsync(Series series);
        Task<List<Series>> GetAllAsync();
        Task<(int, Series?)> GetByIdAsync(int? id);
        Task<int> DeleteByIdAsync(int? id);
        Task<int> UpdateByIDAsync(int? id, Series ser);
    }
}
