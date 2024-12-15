using LibraryProject.ControllerModels;
using LibraryProject.Entities.EntityBook;

namespace LibraryProject.Interfaces
{
    public interface IBookService
    {
        Task AddBookAsync(int? GenreId, int? CategoryId, int? AuthorId, int? SeriesId, BookDTOParent WithoutExternalBook, CancellationToken cancellationToken);
        Task<List<BookDTOChild>> GetAllBooksAsync(CancellationToken cancellationToken);
        Task<BookDTOChild> ReturnBookByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, int? GenreId, int? CategoryId, int? AuthorId, int? SeriesId, BookDTOParent WithoutExternalBook);
    }
}
