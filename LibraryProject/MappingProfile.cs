using AutoMapper;
using LibraryProject.ControllerModels;
using LibraryProject.Entities;
using LibraryProject.Entities.BookProps;
using LibraryProject.Entities.EntityBook;
using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.EntityRewiev;
using LibraryProject.Entities.Orders;
using LibraryProject.Entities.UserApi;

namespace LibraryProject
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookAuthor, AuthorDTO>();
            CreateMap<AuthorDTO, BookAuthor>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<Genre, GenreDTO>();
            CreateMap<GenreDTO, Genre>();

            CreateMap<Series, SeriesDTO>();
            CreateMap<SeriesDTO, Series>();

            CreateMap<Book, BookDTOChild>();
            CreateMap<BookDTOChild, BookDTOChild>();

            CreateMap<BookWithoutExternal, BookDTOParent>();
            CreateMap<BookDTOParent, BookWithoutExternal>();

            CreateMap<BookLoan, LoanDTOChild>();
            CreateMap<LoanDTOChild,  BookLoan>();

            CreateMap<BookLoanWithoutExternal, LoanDTOParent>();
            CreateMap<LoanDTOParent, BookLoanWithoutExternal>();

            CreateMap<BookPurchaseOrder, OrderDTOChild>();
            CreateMap<OrderDTOChild, BookPurchaseOrder>();

            CreateMap<BookPurchaseOrderWithoutExternal, OrderDTOParent>();
            CreateMap<OrderDTOParent, BookPurchaseOrderWithoutExternal>();

            CreateMap<Status, StatusDTO>();
            CreateMap<StatusDTO, Status>();

            CreateMap<Review, ReviewDTOChild>();
            CreateMap<ReviewDTOChild, Review>();

            CreateMap<ReviewWithoutExternal, ReviewDTOParent>();
            CreateMap<ReviewDTOParent, ReviewWithoutExternal>();

            CreateMap<User, UserDTOChild>();
            CreateMap<UserDTOChild, User>();

            CreateMap<UserWithoutExternal, UserDTOParent>();
            CreateMap<UserDTOParent, UserWithoutExternal>();

            CreateMap<UserApi, UserApiDTO>();
            CreateMap<UserApiDTO, UserApi>();
        }
    }
}
