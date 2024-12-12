
using LibraryProject.Entities;
using LibraryProject.Entities.BookProps;
using LibraryProject.Entities.EntityBook;
using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.EntityRewiev;
using LibraryProject.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace LibraryProject.Applications
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Series> BookSeries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<BookPurchaseOrder> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Настройка книг
            //связь с жанрами книг
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId);
            //связь с категориями книг
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);
            //связь с сериями книг
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Series)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.SeriesId);
            //связь с авторами книг
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.AuthorId);

            //Настройка отзывов на книги
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            //Настройка возвращений книг
            //Связь с пользователями
            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.User)
                .WithMany(u => u.Loans)
                .HasForeignKey(bl => bl.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            //Связь с книгами
            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(bl => bl.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            //Настройка заказов
            //Связь с пользователями
            modelBuilder.Entity<BookPurchaseOrder>()
                .HasOne(bpo => bpo.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(bpo => bpo.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            //Связь с книгами
            modelBuilder.Entity<BookPurchaseOrder>()
                .HasOne(bpo => bpo.Book)
                .WithMany(b => b.Orders)
                .HasForeignKey(bpo => bpo.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            //Связь с статусами
            modelBuilder.Entity <BookPurchaseOrder>()
                .HasOne(bpo => bpo.Status)
                .WithMany(s => s.Orders)
                .HasForeignKey(bpo => bpo.StatusId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");
        }
    }
}
