
using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using LibraryProject.Interfaces;
using LibraryProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DatabaseContext>();

            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddTransient<AuthorService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddTransient<BookService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddTransient<CategoryService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddTransient<GenreService>();
            builder.Services.AddScoped<ILoanService, BookLoanService>();
            builder.Services.AddTransient<BookLoanService>();
            builder.Services.AddScoped<IOrderService, BookOrderService>();
            builder.Services.AddTransient<BookOrderService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddTransient<ReviewService>();
            builder.Services.AddScoped<ISeriesService, SeriesService>();
            builder.Services.AddTransient<SeriesService>();
            builder.Services.AddScoped<IStatusService, StatusService>();
            builder.Services.AddTransient<StatusService>();
            builder.Services.AddScoped<IUserService, UserDatabaseSevice>();
            builder.Services.AddTransient<UserDatabaseSevice>();


            builder.Services.AddAutoMapper(typeof(Program));
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
