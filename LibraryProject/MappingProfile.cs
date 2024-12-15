using AutoMapper;
using LibraryProject.ControllerModels;
using LibraryProject.Entities;
using LibraryProject.Entities.BookProps;
using LibraryProject.Entities.EntityBook;
using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.EntityRewiev;
using LibraryProject.Entities.Orders;

namespace LibraryProject
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookAuthor, AuthorModel>();
            CreateMap<AuthorModel, BookAuthor>();

            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();

            CreateMap<Genre, GenreModel>();
            CreateMap<GenreModel, Genre>();

            CreateMap<Series, SeriesModel>();
            CreateMap<SeriesModel, Series>();

            CreateMap<Book, BookModel>();
            CreateMap<BookModel, BookModel>();

            CreateMap<BookWithoutExternal, BookModelWithoutExternal>();
            CreateMap<BookModelWithoutExternal, BookWithoutExternal>();

            CreateMap<BookLoan, LoanModel>();
            CreateMap<LoanModel,  BookLoan>();

            CreateMap<BookLoanWithoutExternal, LoanModelWithoutExternal>();
            CreateMap<LoanModelWithoutExternal, BookLoanWithoutExternal>();

            CreateMap<BookPurchaseOrder, OrderModel>();
            CreateMap<OrderModel, BookPurchaseOrder>();

            CreateMap<BookPurchaseOrderWithoutExternal, OrderModelWithoutExternal>();
            CreateMap<OrderModelWithoutExternal, BookPurchaseOrderWithoutExternal>();

            CreateMap<Status, StatusModel>();
            CreateMap<StatusModel, Status>();

            CreateMap<Review, ReviewModel>();
            CreateMap<ReviewModel, Review>();

            CreateMap<ReviewWithoutExternal, ReviewModelWithoutExternal>();
            CreateMap<ReviewModelWithoutExternal, ReviewWithoutExternal>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            CreateMap<UserWithoutExternal, UserModelWithoutExternal>();
            CreateMap<UserModelWithoutExternal, UserWithoutExternal>();
        }
    }
}
