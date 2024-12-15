using LibraryProject.Entities.BookProps;

namespace LibraryProject.ControllerModels
{
    public class BookModel
    {
        public int GenreId { get; set; }
        public GenreModel Genre { get; set; }

        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }

        public int AuthorId { get; set; }
        public AuthorModel Author { get; set; }

        public int SeriesId { get; set; }
        public SeriesModel Series { get; set; }

        public List<ReviewModel> Reviews { get; set; } = new();
        public List<OrderModel> Orders { get; set; } = new();
        public List<LoanModel> Loans { get; set; } = new();
    }
}
