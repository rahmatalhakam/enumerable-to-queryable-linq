using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace enumerable_to_queryable_linq.Models;

public class MainDocumentBuilder : IEntityTypeConfiguration<MainDocument>
{
    public void Configure(EntityTypeBuilder<MainDocument> builder)
    {
        builder.Property(m => m.DocumentNumber).HasMaxLength(6);
        builder.Property(c => c.DocumentStatus).HasConversion<string>().HasMaxLength(10);
        builder.Property(m => m.Description).HasMaxLength(50);
    }
}

public class TransactionDocumentABuilder : IEntityTypeConfiguration<TransactionDocumentA>
{
    public void Configure(EntityTypeBuilder<TransactionDocumentA> builder)
    {
        builder.Property(m => m.DocumentNumber).HasMaxLength(6);
        builder.Property(m => m.Details).HasMaxLength(20);
        builder.HasOne(x => x.MainDocument).WithOne(x => x.TransactionDocumentA).OnDelete(DeleteBehavior.Cascade);
    }
}

public class TransactionDocumentBBuilder : IEntityTypeConfiguration<TransactionDocumentB>
{
    public void Configure(EntityTypeBuilder<TransactionDocumentB> builder)
    {
        builder.Property(m => m.DocumentNumber).HasMaxLength(6);
        builder.Property(m => m.Details).HasMaxLength(30);
        builder.HasOne(x => x.MainDocument).WithOne(x => x.TransactionDocumentB).OnDelete(DeleteBehavior.Cascade);
    }
}