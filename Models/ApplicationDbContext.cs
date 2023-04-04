using Microsoft.EntityFrameworkCore;

namespace enumerable_to_queryable_linq.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<MainDocument> MainDocuments => Set<MainDocument>();
    public DbSet<TransactionDocumentA> TransactionDocumentAs => Set<TransactionDocumentA>();
    public DbSet<TransactionDocumentB> TransactionDocumentBs => Set<TransactionDocumentB>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new MainDocumentBuilder().Configure(modelBuilder.Entity<MainDocument>());
        new TransactionDocumentABuilder().Configure(modelBuilder.Entity<TransactionDocumentA>());
        new TransactionDocumentBBuilder().Configure(modelBuilder.Entity<TransactionDocumentB>());
    }
}