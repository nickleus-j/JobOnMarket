using JobMarket.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsOnMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
        private IDataWorkUnit UnitOfWork;
        public ContractorController(IDataWorkUnit unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Page and pageSize must be greater than 0.");
            }

            var contractors = await UnitOfWork.ContractorRepository.GetFromPageAsync(page, pageSize, "ID");
            return Ok(contractors);
        }
        [HttpGet("{q}")]
        public async Task<IActionResult> Get(string q)
        {
            var customers = await UnitOfWork.ContractorRepository.SearchContractor(q);
            return Ok(customers);
        }
    }
}
