using LibraryProject.ControllerModels;
using LibraryProject.Entities.EntityBookLoan;

namespace LibraryProject.Interfaces
{
    public interface ILoanService
    {
        Task Add(LoanDTOParent bookLoanWithoutExternal, int? UserId, int? BookId, CancellationToken cancellationToken);
        Task<LoanDTOChild> GetById(int? id, CancellationToken cancellationToken);
        Task<List<LoanDTOChild>> Get(CancellationToken cancellationToken);
        Task Delete(int? id);
        Task Update(LoanDTOParent bookLoanWithoutExternal, int? UserId, int? BookId, int? id);
    }
}
