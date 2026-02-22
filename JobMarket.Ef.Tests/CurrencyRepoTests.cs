using JetBrains.Annotations;
using JobMarket.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace JobMarket.Ef.Tests;

[TestSubject(typeof(CurrencyRepository))]
public class CurrencyRepoTests
{
private JobMarketContext GetInMemoryContext()
    {
        DbContextOptions<JobMarketContext> dbContextOptions = new DbContextOptionsBuilder<JobMarketContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        var context = new JobMarketContext(dbContextOptions);
        // Seed test data
        context.Currency.AddRange(
            new Currency { Id = 1, Code = "USD", Name = "United States Dollar" },
            new Currency { Id = 2, Code = "EUR", Name = "Euro" },
            new Currency { Id = 3, Code = "JPY", Name = "Japanese Yen" }
        );
        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task GetAllCurrencyCodes_ReturnsAllCodes()
    {
        // Arrange
        using (var context = GetInMemoryContext())
        {
            var repo = new CurrencyRepository(context);

            // Act
            var codes = await repo.GetAllCurrencyCodes();

            // Assert
            Assert.True(codes.Count() == 3);
            Assert.Contains("USD", codes);
            Assert.Contains("EUR", codes);
            Assert.Contains("JPY", codes);
        }
    }

    [Fact]
    public async Task GetAllCurrencyNames_ReturnsAllNames()
    {
        // Arrange
        await using (var context = GetInMemoryContext())
        {
            var repo = new CurrencyRepository(context);

            // Act
            var names = await repo.GetAllCurrencyNames();

            // Assert
            Assert.NotEmpty(names);
            Assert.Contains("United States Dollar", names);
            Assert.Contains("Euro", names);
            Assert.Contains("Japanese Yen", names);
        }
    }

    [Fact]
    public async Task GetCurrencyByCode_ReturnsCorrectCurrency()
    {
        // Arrange
        await using (var context = GetInMemoryContext())
        {
            var repo = new CurrencyRepository(context);

            // Act
            var currency = await repo.GetCurrencyByCode("eur");

            // Assert
            Assert.NotNull(currency);
            Assert.Equal("EUR", currency.Code);
            Assert.Equal("Euro", currency.Name);   
        }
    }

    [Fact]
    public async Task GetCurrencyByCode_ReturnsNull_WhenNotFound()
    {
        // Arrange
        await using (var context = GetInMemoryContext())
        {
            var repo = new CurrencyRepository(context);
            // Act
            var currency = await repo.GetCurrencyByCode("GBP");
            // Assert
            Assert.Null(currency);   
        }
    }
}