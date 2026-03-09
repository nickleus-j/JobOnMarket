using JobMarket.Data;
using JobsOnMarket.Dto.Job;
using JobsOnMarket.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobsOnMarket.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobDoneReportController : Controller
{
    private IDataUnitOfWork UnitOfWork;
    public JobDoneReportController(IDataUnitOfWork unitOfWork)
    {
        this.UnitOfWork = unitOfWork;
    }
    [HttpGet("{contractorName}")]
    public async Task<IActionResult> Get(string contractorName,[FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page < 1 || pageSize < 1)
        {
            return BadRequest("Page and pageSize must be greater than 0.");
        }
        var jobreports=await UnitOfWork.JobDoneReportRepository.GetJobDoneReportsOfContractorAsync(contractorName);
        return Ok(JobMapper.MapToJobDoneDtos(jobreports));
    }
    [HttpGet("Job/{jobId}")]
    public async Task<IActionResult> Get(int jobId)
    {
        if (jobId < 1 )
        {
            return BadRequest("Page and pageSize must be greater than 0.");
        }
        var jobreport=await UnitOfWork.JobDoneReportRepository.GetJobDoneReportAsync(jobId);
        if (jobreport == null)
        {
            return NotFound();
        }
        return Ok(JobMapper.MapToJobDoneDto(jobreport));
    }
    [Authorize(Roles = "Customer")]
    [HttpPost]
    public async Task<IActionResult> Post(JobDoneDto dto)
    {
        var jobDone = JobMapper.MapToJobDoneEntity(dto,await UnitOfWork.CurrencyRepository.GetAllAsync());
        await UnitOfWork.JobDoneReportRepository.AddAsync(jobDone);
        await UnitOfWork.CompleteAsync();
        return Ok(JobMapper.MapToJobDoneDto(jobDone));
    }
}