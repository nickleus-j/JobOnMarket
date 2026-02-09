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
        public async Task<IActionResult> Get()
        {
            var customers = await UnitOfWork.ContractorRepository.GetAllAsync();
            return Ok(customers);
        }
        [HttpGet("{q}")]
        public async Task<IActionResult> Get(string q)
        {
            var customers = await UnitOfWork.ContractorRepository.SearchContractor(q);
            return Ok(customers);
        }
    }
}
