using JobMarket.Data;
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
        var currencies = await UnitOfWork.CurrencyRepository.GetAllAsync();
        return Ok(currencies.Select(x=>new CurrencyDto(){Code= x.Code,Name= x.Name }));
    }
    [HttpGet("{code}")]
    public async Task<IActionResult> Get(string code)
    {
        var c = await UnitOfWork.CurrencyRepository.GetCurrencyByCode(code);
        return Ok(new CurrencyDto(){Code= c.Code,Name= c.Name });
    }
}