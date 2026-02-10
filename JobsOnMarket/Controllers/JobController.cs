using JobMarket.Data;
using JobMarket.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsOnMarket.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private IDataWorkUnit UnitOfWork;
        public JobController(IDataWorkUnit unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await UnitOfWork.JobRepository.GetAllAsync());
        }
    }
}
