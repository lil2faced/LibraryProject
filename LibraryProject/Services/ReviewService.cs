using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using LibraryProject.Entities.EntityBook;
using LibraryProject.Entities.EntityRewiev;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LibraryProject.Services
{
    public class ReviewService
    {
        public static async Task<int> AddAsync(DatabaseContext _db, int BookId, ReviewWithoutExternal reviewWithout)
        {
            var book = await _db.Books.FindAsync(BookId);
            if (book == null)
            {
                return 1;
            }

            Review review = new Review()
            {
                BookId = BookId,
                BodyReview = reviewWithout.BodyReview,
                DateReview = reviewWithout.DateReview
            };

            _db.Reviews.Add(review);
            await _db.SaveChangesAsync();

            book.Reviews.Add(review);

            await _db.SaveChangesAsync();

            return 0;
        }
        public static async Task<List<Review>> GetAllReviews(DatabaseContext database)
        {
            return await database.Reviews.ToListAsync();
        }
        public static async Task<Review?> GetReviewById(DatabaseContext databaseContext, int id)
        {
            var review = await databaseContext.Reviews.FindAsync(id);
            return review;
        }
        public static async Task<int> DeleteReviewById(DatabaseContext databaseContext, int id)
        {
            var review = await databaseContext.Reviews.FindAsync(id);
            if (review == null)
            {
                return 1;
            }
            databaseContext.Reviews.Remove(review);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
        public static async Task<int> UpdateReview(DatabaseContext databaseContext, int id, ReviewWithoutExternal rev)
        {
            var review = await databaseContext.Reviews.FindAsync(id);
            if (review == null)
            {
                return 1;
            }
            review.DateReview = rev.DateReview;
            review.BodyReview = rev.BodyReview;
            databaseContext.Reviews.Update(review);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
    }
}
