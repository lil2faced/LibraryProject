using LibraryProject.Entities.BookProps;
using LibraryProject.Entities.EntityBook;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LibraryProject.Applications.EntityConfigurations
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId);
            builder
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);
            builder
                .HasOne(b => b.Series)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.SeriesId);
            builder
                .HasOne(b => b.Author)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.AuthorId);
        }
    }
}
