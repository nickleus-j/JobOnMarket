using JobMarket.Data.Entity;

namespace JobMarket.Data;

public interface IJobDoneReportRepository: IRepository<JobDoneReport>
{
    public Task ReviewDoneJobAsync(int offerId,string description, short rating);
    public Task<JobDoneReport> GetJobDoneReportAsync(int jobId);
    public Task<IEnumerable<JobDoneReport>> GetJobDoneReportsOfContractorAsync(int contractorId);
    public Task<IEnumerable<JobDoneReport>> GetJobDoneReportsOfContractorAsync(string contractorName);
}