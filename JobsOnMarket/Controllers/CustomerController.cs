using JobMarket.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsOnMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IDataWorkUnit UnitOfWork;
        public CustomerController(IDataWorkUnit unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await UnitOfWork.CustomerRepository.GetAllAsync();
            return Ok(customers);
        }
        [HttpGet("{q}")]
        public async Task<IActionResult> Get(string q)
        {
            var customers= await UnitOfWork.CustomerRepository.SearchCustomerAsync(q);
            return Ok(customers);
        }
    }
}
