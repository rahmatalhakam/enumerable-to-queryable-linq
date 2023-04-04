using enumerable_to_queryable_linq.Dtos;
using enumerable_to_queryable_linq.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace enumerable_to_queryable_linq.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public TransactionController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("AddMainDocument")]
    public async Task<ActionResult<MainDocument>> AddMainDocument([FromBody] MainDocumentInput input)
    {
        var result = _dbContext.MainDocuments.Add(new MainDocument
        {
            Description = input.Description,
            DocumentDate = input.DocumentDate,
            DocumentNumber = input.DocumentNumber,
            DocumentStatus = input.DocumentStatus
        });
        await _dbContext.SaveChangesAsync();
        return Ok(result.Entity);
    }

    [HttpPost("AddTransactionDocumentA")]
    public async Task<ActionResult<TransactionDocumentDto>> AddTransactionDocumentA([FromBody] TransactionDocumentInput input)
    {
        var result = _dbContext.TransactionDocumentAs.Add(new TransactionDocumentA
        {
            Details = input.Details,
            DocumentDate = input.DocumentDate,
            DocumentNumber = input.DocumentNumber,
            MainDocumentId = input.MainDocumentId
        });
        await _dbContext.SaveChangesAsync();
        var entity = result.Entity;
        return Ok(new TransactionDocumentDto
        {
            Id = entity.Id,
            Details = entity.Details,
            DocumentDate = entity.DocumentDate,
            DocumentNumber = entity.DocumentNumber,
            MainDocumentId = entity.MainDocumentId
        });
    }

    [HttpPost("AddTransactionDocumentB")]
    public async Task<ActionResult<TransactionDocumentDto>> AddTransactionDocumentB([FromBody] TransactionDocumentInput input)
    {
        var result = _dbContext.TransactionDocumentBs.Add(new TransactionDocumentB
        {
            Details = input.Details,
            DocumentDate = input.DocumentDate,
            DocumentNumber = input.DocumentNumber,
            MainDocumentId = input.MainDocumentId
        });
        await _dbContext.SaveChangesAsync();
        var entity = result.Entity;
        return Ok(new TransactionDocumentDto
        {
            Id = entity.Id,
            Details = entity.Details,
            DocumentDate = entity.DocumentDate,
            DocumentNumber = entity.DocumentNumber,
            MainDocumentId = entity.MainDocumentId
        });
    }

    [HttpGet("GetReportEnumerable")]
    public ActionResult<TransactionReportGroupDto> GetReportEnumerable()
    {
        var subQueryMainDocumentOnly = (
            from md in _dbContext.MainDocuments.AsNoTracking()

            where !_dbContext.TransactionDocumentAs.Any(ta => ta.MainDocumentId == md.Id)
            && !_dbContext.TransactionDocumentBs.Any(tb => tb.MainDocumentId == md.Id)

            select new TransactionReportDto
            {
                MainDocumentStatus = md.DocumentStatus.ToString(),
                MainDocumentNumber = md.DocumentNumber,
                MainDocumentDate = md.DocumentDate.ToString("dd/MM/yyyy"),
                MainDescription = md.Description,
                DocumentNumberA = "",
                DocumentDateA = "",
                DetailsA = "",
                DocumentNumberB = "",
                DocumentDateB = "",
                DetailsB = ""
            }
        ).AsEnumerable();

        var subQueryTransactionA = (
            from md in _dbContext.MainDocuments.AsNoTracking()
            join ta in _dbContext.TransactionDocumentAs on md.Id equals ta.MainDocumentId

            select new TransactionReportDto
            {
                MainDocumentStatus = md.DocumentStatus.ToString(),
                MainDocumentNumber = md.DocumentNumber,
                MainDocumentDate = md.DocumentDate.ToString("dd/MM/yyyy"),
                MainDescription = md.Description,
                DocumentNumberA = ta.DocumentNumber,
                DocumentDateA = ta.DocumentDate.ToString("dd/MM/yyyy"),
                DetailsA = ta.Details,
                DocumentNumberB = "",
                DocumentDateB = "",
                DetailsB = ""
            }
        ).AsEnumerable();

        var subQueryTransactionB = (
            from md in _dbContext.MainDocuments.AsNoTracking()
            join tb in _dbContext.TransactionDocumentBs on md.Id equals tb.MainDocumentId

            select new TransactionReportDto
            {
                MainDocumentStatus = md.DocumentStatus.ToString(),
                MainDocumentNumber = md.DocumentNumber,
                MainDocumentDate = md.DocumentDate.ToString("dd/MM/yyyy"),
                MainDescription = md.Description,
                DocumentNumberA = "",
                DocumentDateA = "",
                DetailsA = "",
                DocumentNumberB = tb.DocumentNumber,
                DocumentDateB = tb.DocumentDate.ToString("dd/MM/yyyy"),
                DetailsB = tb.Details,
            }
        ).AsEnumerable();

        var result = subQueryMainDocumentOnly
            .Union(subQueryTransactionA)
            .Union(subQueryTransactionB)
            .GroupBy(x => x.MainDocumentStatus)
            .Select(x => new TransactionReportGroupDto
            {
                MainDocumentStatus = x.Key,
                Reports = x.ToList()
            });
        return Ok(result);
    }

    [HttpGet("GetReportQueryableErrorRunTime")]
    public ActionResult<TransactionReportGroupDto> GetReportQueryableErrorRunTime()
    {
        var subQueryMainDocumentOnly = (
            from md in _dbContext.MainDocuments.AsNoTracking()

            where !_dbContext.TransactionDocumentAs.Any(ta => ta.MainDocumentId == md.Id)
            && !_dbContext.TransactionDocumentBs.Any(tb => tb.MainDocumentId == tb.Id)

            select new TransactionReportDto
            {
                MainDocumentStatus = md.DocumentStatus.ToString(),
                MainDocumentNumber = md.DocumentNumber,
                MainDocumentDate = md.DocumentDate.ToString("dd/MM/yyyy"),
                MainDescription = md.Description,
                DocumentNumberA = "",
                DocumentDateA = "",
                DetailsA = "",
                DocumentNumberB = "",
                DocumentDateB = "",
                DetailsB = ""
            }
        ).AsQueryable();

        var subQueryTransactionA = (
            from md in _dbContext.MainDocuments.AsNoTracking()
            join ta in _dbContext.TransactionDocumentAs on md.Id equals ta.MainDocumentId

            select new TransactionReportDto
            {
                MainDocumentStatus = md.DocumentStatus.ToString(),
                MainDocumentNumber = md.DocumentNumber,
                MainDocumentDate = md.DocumentDate.ToString("dd/MM/yyyy"),
                MainDescription = md.Description,
                DocumentNumberA = ta.DocumentNumber,
                DocumentDateA = ta.DocumentDate.ToString("dd/MM/yyyy"),
                DetailsA = ta.Details,
                DocumentNumberB = "",
                DocumentDateB = "",
                DetailsB = ""
            }
        ).AsQueryable();

        var subQueryTransactionB = (
            from md in _dbContext.MainDocuments.AsNoTracking()
            join tb in _dbContext.TransactionDocumentBs on md.Id equals tb.MainDocumentId

            select new TransactionReportDto
            {
                MainDocumentStatus = md.DocumentStatus.ToString(),
                MainDocumentNumber = md.DocumentNumber,
                MainDocumentDate = md.DocumentDate.ToString("dd/MM/yyyy"),
                MainDescription = md.Description,
                DocumentNumberA = "",
                DocumentDateA = "",
                DetailsA = "",
                DocumentNumberB = tb.DocumentNumber,
                DocumentDateB = tb.DocumentDate.ToString("dd/MM/yyyy"),
                DetailsB = tb.Details,
            }
        ).AsQueryable();

        var result = subQueryMainDocumentOnly
            .Union(subQueryTransactionA)
            .Union(subQueryTransactionB)
            .GroupBy(x => x.MainDocumentStatus)
            .Select(x => new TransactionReportGroupDto
            {
                MainDocumentStatus = x.Key,
                Reports = x.ToList()
            });
        return Ok(result);
    }

    [HttpGet("GetReportQueryable")]
    public ActionResult<TransactionReportGroupDto> GetReportQueryable()
    {
        var subQueryMainDocumentOnly = (
            from md in _dbContext.MainDocuments.AsNoTracking()

            where !_dbContext.TransactionDocumentAs.Any(ta => ta.MainDocumentId == md.Id)
            && !_dbContext.TransactionDocumentBs.Any(tb => tb.MainDocumentId == tb.Id)

            select new
            {
                MainDocumentStatus = md.DocumentStatus,
                MainDocumentNumber = md.DocumentNumber,
                MainDocumentDate = md.DocumentDate,
                MainDescription = md.Description,
                DocumentNumberA = "",
                DocumentDateA = "",
                DetailsA = "",
                DocumentNumberB = "",
                DocumentDateB = "",
                DetailsB = ""
            }
        ).AsQueryable();

        var subQueryTransactionA = (
            from md in _dbContext.MainDocuments.AsNoTracking()
            join ta in _dbContext.TransactionDocumentAs on md.Id equals ta.MainDocumentId

            select new
            {
                MainDocumentStatus = md.DocumentStatus,
                MainDocumentNumber = md.DocumentNumber,
                MainDocumentDate = md.DocumentDate,
                MainDescription = md.Description,
                DocumentNumberA = Convert.ToString(ta.DocumentNumber),
                DocumentDateA = ta.DocumentDate.ToString(),
                DetailsA = Convert.ToString(ta.Details),
                DocumentNumberB = "",
                DocumentDateB = "",
                DetailsB = ""
            }
        ).AsQueryable();

        var subQueryTransactionB = (
            from md in _dbContext.MainDocuments.AsNoTracking()
            join tb in _dbContext.TransactionDocumentBs on md.Id equals tb.MainDocumentId

            select new
            {
                MainDocumentStatus = md.DocumentStatus,
                MainDocumentNumber = md.DocumentNumber,
                MainDocumentDate = md.DocumentDate,
                MainDescription = md.Description,
                DocumentNumberA = "",
                DocumentDateA = "",
                DetailsA = "",
                DocumentNumberB = Convert.ToString(tb.DocumentNumber),
                DocumentDateB = tb.DocumentDate.ToString(),
                DetailsB = Convert.ToString(tb.Details),
            }
        ).AsQueryable();

        var result = subQueryMainDocumentOnly
            .Union(subQueryTransactionA)
            .Union(subQueryTransactionB)
            .Select(x => new TransactionReportDto
            {
                MainDocumentStatus = x.MainDocumentStatus.ToString(),
                MainDocumentNumber = x.MainDocumentNumber,
                MainDocumentDate = x.MainDocumentDate.ToString("dd/MM/yyyy"),
                MainDescription = x.MainDescription,
                DocumentNumberA = x.DocumentNumberA,
                DocumentDateA = x.DocumentDateA,
                DetailsA = x.DetailsA,
                DocumentNumberB = x.DocumentNumberB,
                DocumentDateB = x.DocumentDateB,
                DetailsB = x.DetailsB,
            })
            .ToList()
            .GroupBy(x => x.MainDocumentStatus)
            .Select(x => new TransactionReportGroupDto
            {
                MainDocumentStatus = x.Key.ToString(),
                Reports = x.ToList()
            });
        return Ok(result);
    }


}