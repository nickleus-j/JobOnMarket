using System.Text;
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
    public static JobOfferDto MapToDto(JobOffer offer, IEnumerable<Currency> currencies)
    {
        if (offer == null) throw new ArgumentNullException(nameof(offer));

        // Resolve currency ID back to code
        string? currencyCode = currencies
            .FirstOrDefault(c => c.Id == offer.PriceCurrencyId)
            ?.Code;

        return new JobOfferDto
        {
            ID = offer.ID,
            CurrencyCode = currencyCode ?? string.Empty,
            JobId = offer.JobId,
            Price = offer.Price,
        };
    }
    public static JobOffer MapToJobOffer(JobOfferDto dto, IEnumerable<Currency> currencies)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        // Try to resolve currency code to ID
        var currency = currencies
            .FirstOrDefault(c => string.Equals(c.Code, dto.CurrencyCode, StringComparison.OrdinalIgnoreCase));
        int? currencyId = currency?.Id;

        return new JobOffer
        {
            ID = dto.ID,
            PriceCurrency = currency,
            JobId = dto.JobId,
            Price = dto.Price,
            PriceCurrencyId = currencyId,
            OfferedByContractorId =  null,
        };
    }
    public static IEnumerable<JobOfferDto> MapToDtos(
        IEnumerable<JobOffer> offers,
        IEnumerable<Currency> currencies)
    {
        if (offers == null) throw new ArgumentNullException(nameof(offers));

        return offers.Select(job => JobMapper.MapToDto(job, currencies));
    }
    public static IEnumerable<JobOffer> MapToJoboffers(
        IEnumerable<JobOfferDto> dtos,
        IEnumerable<Currency> currencies)
    {
        if (dtos == null) throw new ArgumentNullException(nameof(dtos));

        return dtos.Select(dto => JobMapper.MapToJobOffer(dto, currencies));
    }
    public static JobDoneDto MapToJobDoneDto(JobDoneReport entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        var customer = entity.OfferCompleted?.OfferedJob?.AcceptedBy;
        StringBuilder sb=new StringBuilder();
        if (customer != null)
        {
            sb.Append(customer.FirstName);
            sb.Append(" ");
            sb.Append(customer.LastName);
        }
        else
        {
            sb.Append(String.Empty);
        }
        return new JobDoneDto
        {
            ID = entity.ID,
            Rating = entity.Rating,
            Description = entity.Description,
            DateReported = entity.DateReported,
            price = entity.OfferCompleted?.Price ?? 0,
            CurrencyCode = entity.OfferCompleted?.PriceCurrency?.Code ?? string.Empty,
            JobId = entity.OfferCompleted?.JobId ?? 0,
            CustomerName = sb.ToString(),
            ContractorName = entity.OfferCompleted?.OfferedByContractor?.Name ?? string.Empty
        };
    }
    public static IEnumerable<JobDoneDto> MapToJobDoneDtos(IEnumerable<JobDoneReport> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        return entities.Select(entity => MapToJobDoneDto(entity));
    }
    public static JobDoneReport MapToJobDoneEntity(JobDoneDto dto, IEnumerable<Currency> currencies)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        // Split CustomerName back into First and Last Name (Assuming "First Last" format)
        string firstName = string.Empty;
        string lastName = string.Empty;

        if (!string.IsNullOrWhiteSpace(dto.CustomerName))
        {
            var parts = dto.CustomerName.Trim().Split(' ', 2);
            firstName = parts[0];
            lastName = parts.Length > 1 ? parts[1] : string.Empty;
        }
        var currency = currencies
            .FirstOrDefault(c => string.Equals(c.Code, dto.CurrencyCode, StringComparison.OrdinalIgnoreCase));
        return new JobDoneReport
        {
            ID = dto.ID,
            Rating = dto.Rating,
            Description = dto.Description,
            DateReported = dto.DateReported,
        
            // Mapping back into the navigation hierarchy
            OfferCompleted = new JobOffer
            {
                ID = dto.JobId, // Mapping DTO JobId to the Entity ID of the offer
                Price = dto.price,
                PriceCurrency = new Currency { Code = currency.Code, Name = currency.Name },
                OfferedByContractor = new Contractor { Name = dto.ContractorName },
                OfferedJob = new Job 
                { 
                    AcceptedBy = new Customer 
                    { 
                        FirstName = firstName, 
                        LastName = lastName 
                    } 
                }
            }
        };
    }

    public static IEnumerable<JobDoneReport> MapToJobDoneEntities(IEnumerable<JobDoneDto> dtos, IEnumerable<Currency> currencies)
    {
        if (dtos == null)
            throw new ArgumentNullException(nameof(dtos));

        return dtos.Select(dto => MapToJobDoneEntity(dto,currencies)).ToList();
    }
}