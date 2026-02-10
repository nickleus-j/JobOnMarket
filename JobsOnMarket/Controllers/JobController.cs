using JobMarket.Data;
using JobMarket.Data.Entity;
using JobMarket.Ef;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsOnMarket.Controllers
{
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
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var job=await UnitOfWork.JobRepository.GetAsync(id);
            return Ok(job);
        }
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Job entity)
        {
            await UnitOfWork.JobRepository.AddAsync(entity);
            await UnitOfWork.CompleteAsync();
            return Ok(entity);
        }
        [Authorize(Roles = "Customer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Job entity)
        {
            try
            {
                UnitOfWork.JobRepository.Update(entity);
            }
            catch (InvalidOperationException ibe)
            {
                return BadRequest( ibe.Message);
            }

            await UnitOfWork.CompleteAsync();
            return Ok(entity);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await UnitOfWork.JobRepository.RemoveJob(id);
            }
            catch (InvalidOperationException ibe)
            {
                return BadRequest(ibe.Message);
            }
            return Ok();
        }
    }
}
