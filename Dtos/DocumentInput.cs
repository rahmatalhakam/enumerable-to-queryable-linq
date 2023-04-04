using enumerable_to_queryable_linq.Contract;

namespace enumerable_to_queryable_linq.Dtos;

public class MainDocumentInput
{
    public DocumentStatus DocumentStatus { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class TransactionDocumentInput
{
    public Guid MainDocumentId { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; }
    public string Details { get; set; } = string.Empty;
}

public class TransactionDocumentDto
{
    public Guid Id { get; set; }
    public Guid MainDocumentId { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; }
    public string Details { get; set; } = string.Empty;
}
