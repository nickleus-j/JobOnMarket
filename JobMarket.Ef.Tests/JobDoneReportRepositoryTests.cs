using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using JobMarket.Data.Entity;
using JobMarket.Ef;

public class JobDoneReportRepositoryTests
{
    private DbContextOptions<JobMarketContext> CreateNewContextOptions()
    {
        // Generates a unique internal database name for every test run
        return new DbContextOptionsBuilder<JobMarketContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task ReviewDoneJobAsync_ShouldAddNewReportToDatabase()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new JobMarketContext(options);
        var repository = new JobDoneReportRepository(context);

        int offerId = 101;
        string description = "Excellent quality.";
        short rating = 5;

        // Act
        await repository.ReviewDoneJobAsync(offerId, description, rating);

        // Assert
        var savedReport = await context.JobDoneReport.FirstOrDefaultAsync();
        Assert.NotNull(savedReport);
        Assert.Equal(offerId, savedReport.JobOfferId);
        Assert.Equal(description, savedReport.Description);
        Assert.Equal(rating, savedReport.Rating);
        // Verify date was set to roughly 'now'
        Assert.True((DateTime.UtcNow - savedReport.DateReported).TotalSeconds < 5);
    }

    [Fact]
    public async Task GetJobDoneReportAsync_WhenJobExists_ReturnsCorrectReport()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new JobMarketContext(options);
        var customer = new Customer { ID = 3, FirstName = "C", LastName = "U" };
        var jobAccepted = new Job { ID = 500, Description = "Accepted job", AcceptedById = 1, AcceptedBy = customer };

        var offerForAcceptedJob = new JobOffer { ID = 1, JobId = jobAccepted.ID, OfferedJob = jobAccepted };
        var report = new JobDoneReport { JobOfferId = 1, Description = "Task Complete" };
        
        context.JobOffer.Add(offerForAcceptedJob);
        context.JobDoneReport.Add(report);
        await context.SaveChangesAsync();

        var repository = new JobDoneReportRepository(context);

        // Act
        var result = await repository.GetJobDoneReportAsync(500);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.JobOfferId);
        Assert.Equal("Task Complete", result.Description);
    }

    [Fact]
    public async Task GetJobDoneReportAsync_WhenJobDoesNotExist_ReturnsNull()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new JobMarketContext(options);
        var repository = new JobDoneReportRepository(context);

        // Act
        var result = await repository.GetJobDoneReportAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetJobDoneReportsOfContractorAsync_ById_ReturnsFilteredList()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new JobMarketContext(options);
        
        int targetContractorId = 10;
        var reports = new List<JobDoneReport>
        {
            new JobDoneReport { OfferCompleted = new JobOffer { OfferedByContractorId = targetContractorId },Description = "Task Complete" },
            new JobDoneReport { OfferCompleted = new JobOffer { OfferedByContractorId = targetContractorId },Description = "Task Complete" },
            new JobDoneReport { OfferCompleted = new JobOffer { OfferedByContractorId = 55 } ,Description = "Task Complete"} 
        };
        
        context.JobDoneReport.AddRange(reports);
        await context.SaveChangesAsync();

        var repository = new JobDoneReportRepository(context);

        // Act
        var result = await repository.GetJobDoneReportsOfContractorAsync(targetContractorId);
        var resultList = result.ToList();

        // Assert
        Assert.Equal(2, resultList.Count);
        Assert.All(resultList, r => Assert.Equal(targetContractorId, r.OfferCompleted.OfferedByContractorId));
    }

    [Fact]
    public async Task GetJobDoneReportsOfContractorAsync_ByName_HandlesCaseInsensitivity()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using (var context = new JobMarketContext(options)){
        
        string contractorName = "AliceSmith";
        var contractor = new Contractor { Name = contractorName };
        var customer = new Customer { ID = 3, FirstName = "C", LastName = "U" };
        var jobAccepted = new Job { ID = 500, Description = "Accepted job", AcceptedById = 1, AcceptedBy = customer };

        var report = new JobDoneReport 
        { 
            OfferCompleted = new JobOffer { OfferedByContractor = contractor 
                ,JobId = jobAccepted.ID
                , OfferedJob = jobAccepted} 
            ,Description = "Task Complete"
        };
        context.JobDoneReport.Add(report);
        await context.SaveChangesAsync();

        var repository = new JobDoneReportRepository(context);

        // Act - Use lowercase search term to test .ToLower() logic in repository
        var result = await repository.GetJobDoneReportsOfContractorAsync("alicesmith");

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(contractorName, result.First().OfferCompleted.OfferedByContractor.Name);
        }
    }
}