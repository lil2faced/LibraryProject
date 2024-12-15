using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryProject.Applications.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<BookPurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<BookPurchaseOrder> builder)
        {
            builder.HasOne(bpo => bpo.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(bpo => bpo.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(bpo => bpo.Book)
                .WithMany(b => b.Orders)
                .HasForeignKey(bpo => bpo.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(bpo => bpo.Status)
                .WithMany(s => s.Orders)
                .HasForeignKey(bpo => bpo.StatusId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
