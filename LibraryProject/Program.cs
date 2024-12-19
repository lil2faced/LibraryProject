
using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using LibraryProject.Interfaces;
using LibraryProject.Middlewares;
using LibraryProject.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.KEY))
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["auth-token"];

                            return Task.CompletedTask;
                        }
                    };
                });
            builder.Services.AddAuthorization();


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
            builder.Services.AddScoped<IUserApiService, UserApiService>();
            builder.Services.AddTransient<UserApiService>();
            builder.Services.AddTransient<JwtProvider>();

            builder.Services.AddTransient<CancellationTokenSource>();

            builder.Services.AddAutoMapper(typeof(Program));
            var app = builder.Build();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.MapControllers();

            app.Run();
        }
    }
}
