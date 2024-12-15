

using LibraryProject.ControllerModels;

namespace LibraryProject.Interfaces
{
    public interface IReviewService
    {
        Task AddAsync(int? BookId, ReviewDTOParent reviewWithout, CancellationToken cancellationToken);
        Task<List<ReviewDTOChild>> GetAllReviews(CancellationToken cancellationToken);
        Task<ReviewDTOChild> GetReviewById(int? id, CancellationToken cancellationToken);
        Task DeleteReviewById(int? id);
        Task UpdateReview(int? id, ReviewDTOParent rev);
    }
}
