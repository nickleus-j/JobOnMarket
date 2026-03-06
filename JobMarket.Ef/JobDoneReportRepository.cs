using JobMarket.Data;
using JobMarket.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace JobMarket.Ef;

public class JobDoneReportRepository: Repository<JobDoneReport>, IJobDoneReportRepository
{
    public JobDoneReportRepository(DbContext context) : base(context)
    {
    }
    private JobMarketContext marketContext
    {
        get { return Context as JobMarketContext; }
    }
    public async Task ReviewDoneJobAsync(int offerId, string description, short rating)
    {
        JobDoneReport report = new JobDoneReport
        {
            DateReported = DateTime.UtcNow,
            Description = description,
            Rating = rating,
            JobOfferId = offerId,
        };
        await AddAsync(report);
        await marketContext.SaveChangesAsync();
    }

    public async Task<JobDoneReport> GetJobDoneReportAsync(int jobId)
    {
        var job=await marketContext.JobOffer.FirstAsync(j=>j.JobId == jobId);
        return await marketContext.JobDoneReport.Include(r=>r.OfferCompleted)
            .Include(r=>r.OfferCompleted.OfferedJob)
            .SingleAsync(r => r.JobOfferId == job.ID);
    }

    public async Task<IEnumerable<JobDoneReport>> GetJobDoneReportsOfContractorAsync(int contractorId)
    {
        return await marketContext.JobDoneReport.Include(r=>r.OfferCompleted)
            .Where(r=>r.OfferCompleted!=null &&r.OfferCompleted.OfferedByContractorId==contractorId).ToListAsync();
    }

    public async Task<IEnumerable<JobDoneReport>> GetJobDoneReportsOfContractorAsync(string contractorName)
    {
        return await marketContext.JobDoneReport.Include(r=>r.OfferCompleted)
            .Include(r=>r.OfferCompleted.OfferedByContractor)
            .Where(r=>r.OfferCompleted!=null &&r.OfferCompleted.OfferedByContractor.Name.ToLower()==contractorName.ToLower())
            .ToListAsync();
    }
}