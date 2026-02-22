using JobMarket.Data;
using JobMarket.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace JobMarket.Ef;

public class CurrencyRepository:Repository<Currency>,ICurrencyRepository
{
    public CurrencyRepository(DbContext context) : base(context)
    {
    }
    private JobMarketContext marketContext
    {
        get { return Context as JobMarketContext; }
    }
    public async Task<IEnumerable<string>> GetAllCurrencyCodes()
    {
        return await marketContext.Currency.Select(x => x.Code).ToListAsync();
    }

    public async Task<IEnumerable<string>> GetAllCurrencyNames()
    {
        return await marketContext.Currency.Select(x => x.Name).ToListAsync();
    }

    public async Task<Currency> GetCurrencyByCode(string code)
    {
        string captilalizedCode = code.ToUpper();
        return await marketContext.Currency.FirstOrDefaultAsync(x => x.Code == captilalizedCode);
    }
}