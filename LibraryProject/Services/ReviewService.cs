using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using LibraryProject.Entities.EntityBook;
using LibraryProject.Entities.EntityRewiev;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using LibraryProject.Interfaces;
using AutoMapper;
using LibraryProject.ControllerModels;

namespace LibraryProject.Services
{
    public class ReviewService : IReviewService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IMapper _mapper;
        public ReviewService(DatabaseContext databaseContext, IMapper mapper)
        {
            _mapper = mapper;
            this.databaseContext = databaseContext;
        }
        public async Task AddAsync(int? BookId, ReviewDTOParent reviewWithout, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (BookId == null || reviewWithout == null)
            {
                throw new ArgumentNullException();
            }
            var book = await databaseContext.Books.FindAsync(BookId);
            if (book == null)
            {
                throw new Exception("Книга не найдена");
            }

            Review review = new Review()
            {
                BookId = (int)BookId,
                BodyReview = reviewWithout.BodyReview,
                DateReview = reviewWithout.DateReview
            };

            databaseContext.Reviews.Add(review);
            await databaseContext.SaveChangesAsync();

            book.Reviews.Add(review);

            await databaseContext.SaveChangesAsync();
        }
        public async Task<List<ReviewDTOChild>> GetAllReviews(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            return await databaseContext.Reviews.Select(p => _mapper.Map<ReviewDTOChild>(p)).ToListAsync();
        }
        public async Task<ReviewDTOChild> GetReviewById(int? id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var review = await databaseContext.Reviews.FindAsync(id);
            return _mapper.Map<ReviewDTOChild>(review);
        }
        public async Task DeleteReviewById(int? id)
        {
            if (id == null) throw new ArgumentNullException();
            var review = await databaseContext.Reviews.FindAsync(id);
            if (review == null)
            {
                throw new Exception("Отзыв не найден");
            }
            databaseContext.Reviews.Remove(review);
            await databaseContext.SaveChangesAsync();
        }
        public async Task UpdateReview(int? id, ReviewDTOParent rev)
        {
            if (id == null || rev == null) throw new ArgumentNullException();
            var review = await databaseContext.Reviews.FindAsync(id);
            if (review == null)
            {
                throw new Exception("Отзыв не найден");
            }
            review.DateReview = rev.DateReview;
            review.BodyReview = rev.BodyReview;
            databaseContext.Reviews.Update(review);
            await databaseContext.SaveChangesAsync();
        }
    }
}
