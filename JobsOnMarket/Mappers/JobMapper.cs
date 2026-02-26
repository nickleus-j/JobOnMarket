using JobMarket.Data.Entity;
using JobsOnMarket.Dto;
using JobsOnMarket.Dto.Job;

namespace JobsOnMarket.Mappers;

public class JobMapper
{
    /// <summary>
    /// Maps a CreateEditJobDto to a Job entity.
    /// </summary>
    /// <param name="dto">The DTO containing job creation/edit data.</param>
    /// <param name="currencies">List of available currencies with IDs.</param>
    /// <returns>A Job entity populated from the DTO.</returns>
    public static Job MapToJob(JobDto dto, IEnumerable<Currency> currencies)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        // Try to resolve currency code to ID
        var currency = currencies
            .FirstOrDefault(c => string.Equals(c.Code, dto.CurrencyCode, StringComparison.OrdinalIgnoreCase));
        int? currencyId = currency?.Id;

        return new Job
        {
            ID = dto.ID,
            StartDate = dto.StartDate,
            DueDate = dto.DueDate,
            Budget = dto.Budget,
            BudgetCurrencyId = currencyId,
            Description = dto.Description,
            AcceptedById = null, // left null until accepted
            AcceptedBy = null,
            BudgetCurrency = null // EF will populate via BudgetCurrencyId
        };
    }
    public static JobDto MapToDto(Job job, IEnumerable<Currency> currencies)
    {
        if (job == null) throw new ArgumentNullException(nameof(job));

        // Resolve currency ID back to code
        string? currencyCode = currencies
            .FirstOrDefault(c => c.Id == job.BudgetCurrencyId)
            ?.Code;

        return new JobDto
        {
            ID = job.ID,
            StartDate = job.StartDate,
            DueDate = job.DueDate,
            Budget = job.Budget,
            CurrencyCode = currencyCode ?? string.Empty,
            Description = job.Description
        };
    }
    public static IEnumerable<Job> MapToJobs(
        IEnumerable<JobDto> dtos,
        IEnumerable<Currency> currencies)
    {
        if (dtos == null) throw new ArgumentNullException(nameof(dtos));

        return dtos.Select(dto => JobMapper.MapToJob(dto, currencies));
    }

    public static IEnumerable<JobDto> MapToDtos(
        IEnumerable<Job> jobs,
        IEnumerable<Currency> currencies)
    {
        if (jobs == null) throw new ArgumentNullException(nameof(jobs));

        return jobs.Select(job => JobMapper.MapToDto(job, currencies));
    }
    public static JobWithCurrencyDto MapToDtoWithCurrency(Job job, IEnumerable<Currency> currencies)
    {
        if (job == null) throw new ArgumentNullException(nameof(job));

        // Resolve currency ID back to code
        Currency? currency = currencies
            .FirstOrDefault(c => c.Id == job.BudgetCurrencyId);

        return new JobWithCurrencyDto
        {
            ID = job.ID,
            StartDate = job.StartDate,
            DueDate = job.DueDate,
            Budget = job.Budget,
            Currency = new CurrencyDto{Code = currency.Code,Name = currency.Name},
            Description = job.Description
        };
    }
}