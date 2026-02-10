using JobMarket.Data;
using JobMarket.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsOnMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOfferController : ControllerBase
    {
        private IDataWorkUnit UnitOfWork;
        public JobOfferController(IDataWorkUnit unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await UnitOfWork.JobOfferRepository.GetAllAsync());
        }
        [HttpGet("Available")]
        public async Task<ActionResult> GetAvailable()
        {
            return Ok(await UnitOfWork.JobOfferRepository.OffersForJobsNotAcceptedYetAsync());
        }
        [Authorize(Roles = "Contractor")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] JobOffer entity)
        {
            await UnitOfWork.JobOfferRepository.AddAsync(entity);
            await UnitOfWork.CompleteAsync();
            return Ok(entity);
        }
        [Authorize(Roles = "Contractor")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] JobOffer entity)
        {
            try
            {
                UnitOfWork.JobOfferRepository.Update(entity);
            }
            catch (InvalidOperationException ibe)
            {
                return BadRequest(ibe.Message);
            }

            await UnitOfWork.CompleteAsync();
            return Ok(entity);
        }
        [Authorize]
        [HttpDelete()]
        public async Task<ActionResult> Delete([FromBody] JobOffer entity)
        {
            try
            {
                UnitOfWork.JobOfferRepository.Remove(entity);
                await UnitOfWork.CompleteAsync();
            }
            catch (InvalidOperationException ibe)
            {
                return BadRequest(ibe.Message);
            }
            return Ok();
        }
    }
}
