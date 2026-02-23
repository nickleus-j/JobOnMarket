using JobMarket.Data;
using JobMarket.Data.Entity;
using JobsOnMarket.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JobsOnMarket.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CurrencyController : Controller
{
    private IDataUnitOfWork UnitOfWork;
    public CurrencyController(IDataUnitOfWork unitOfWork)
    {
        this.UnitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        IEnumerable<Currency> currencies = await UnitOfWork.CurrencyRepository.GetAllAsync();
        return Ok(currencies.Select(c=>new CurrencyDto(){Code= c.Code,Name= c.Name }));
    }
    [HttpGet("{code}")]
    public async Task<IActionResult> Get(string code)
    {
        Currency c = await UnitOfWork.CurrencyRepository.GetCurrencyByCode(code);
        return Ok(new CurrencyDto(){Code= c.Code,Name= c.Name });
    }
}