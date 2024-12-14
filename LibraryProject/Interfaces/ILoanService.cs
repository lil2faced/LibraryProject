using LibraryProject.Entities.EntityBookLoan;

namespace LibraryProject.Interfaces
{
    public interface ILoanService
    {
        Task Add(BookLoanWithoutExternal bookLoanWithoutExternal, int? UserId, int? BookId, CancellationToken cancellationToken);
        Task<BookLoan> GetById(int? id, CancellationToken cancellationToken);
        Task<List<BookLoan>> Get(CancellationToken cancellationToken);
        Task Delete(int? id);
        Task Update(BookLoanWithoutExternal bookLoanWithoutExternal, int? UserId, int? BookId, int? id);
    }
}
