using JobMarket.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsOnMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IDataUnitOfWork UnitOfWork;
        public CustomerController(IDataUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int page=1, [FromQuery] int pageSize=10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Page and pageSize must be greater than 0.");
            }

            var customers = await UnitOfWork.CustomerRepository.GetFromPageAsync(page,pageSize,"ID");
            return Ok(customers);
        }
        [HttpGet("{q}")]
        public async Task<IActionResult> Get(string q)
        {
            var customers= await UnitOfWork.CustomerRepository.SearchCustomerAsync(q);
            return Ok(customers);
        }
        [HttpGet("{q}/{page}")]
        public async Task<IActionResult> Get(string q, int page, [FromQuery] int pageSize = 10)
        {
            var customers = await UnitOfWork.CustomerRepository.SearchCustomerAsync(q, page, pageSize);
            return Ok(customers);
        }
    }
}
