using LibraryProject.Entities.EntityBook;
using LibraryProject.Entities.EntityBookLoan;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryProject.Applications.EntityConfigurations
{
    public class LoanEntityTypeConfiguration : IEntityTypeConfiguration<BookLoan>
    {
        public void Configure(EntityTypeBuilder<BookLoan> builder)
        {
            builder.HasOne(bl => bl.User)
                .WithMany(u => u.Loans)
                .HasForeignKey(bl => bl.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(bl => bl.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(bl => bl.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
