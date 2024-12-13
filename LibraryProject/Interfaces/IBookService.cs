using LibraryProject.Entities.EntityBook;

namespace LibraryProject.Interfaces
{
    public interface IBookService
    {
        Task AddBookAsync(int? GenreId, int? CategoryId, int? AuthorId, int? SeriesId, BookWithoutExternal WithoutExternalBook, CancellationToken cancellationToken);
        Task<List<Book>> GetAllBooksAsync(CancellationToken cancellationToken);
        Task<Book> ReturnBookByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, int? GenreId, int? CategoryId, int? AuthorId, int? SeriesId, BookWithoutExternal WithoutExternalBook);
    }
}
