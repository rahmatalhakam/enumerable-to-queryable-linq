using enumerable_to_queryable_linq.Contract;

namespace enumerable_to_queryable_linq.Models;

public class MainDocument
{
    public Guid Id { get; set; }
    public DocumentStatus DocumentStatus { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public virtual TransactionDocumentA? TransactionDocumentA { get; set; }
    public virtual TransactionDocumentB? TransactionDocumentB { get; set; }
}

public class TransactionDocumentA
{
    public Guid Id { get; set; }
    public Guid MainDocumentId { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; }
    public string Details { get; set; } = string.Empty;
    public virtual MainDocument? MainDocument { get; set; }
}

public class TransactionDocumentB
{
    public Guid Id { get; set; }
    public Guid MainDocumentId { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; }
    public string Details { get; set; } = string.Empty;
    public virtual MainDocument? MainDocument { get; set; }
}