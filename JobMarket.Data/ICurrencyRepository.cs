using JobMarket.Data.Entity;

namespace JobMarket.Data;

public interface ICurrencyRepository:IRepository<Currency>
{
    Task<IEnumerable<string>> GetAllCurrencyCodes();
    Task<IEnumerable<string>> GetAllCurrencyNames();
    Task<Currency> GetCurrencyByCode(string code);
}