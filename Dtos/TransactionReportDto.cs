namespace enumerable_to_queryable_linq.Dtos;
public class TransactionReportDto
{
    public string MainDocumentStatus { get; set; } = string.Empty;
    public string MainDocumentNumber { get; set; } = string.Empty;
    public string MainDocumentDate { get; set; } = string.Empty;
    public string MainDescription { get; set; } = string.Empty;

    public string DocumentNumberA { get; set; } = string.Empty;
    public string DocumentDateA { get; set; } = string.Empty;
    public string DetailsA { get; set; } = string.Empty;

    public string DocumentNumberB { get; set; } = string.Empty;
    public string DocumentDateB { get; set; } = string.Empty;
    public string DetailsB { get; set; } = string.Empty;
}

public class TransactionReportGroupDto
{
    public string MainDocumentStatus { get; set; } = string.Empty;
    public List<TransactionReportDto> Reports { get; set; } = new();

}