using JobMarket.Data;
using JobMarket.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsOnMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOfferController : ControllerBase
    {
        private IDataUnitOfWork UnitOfWork;
        public JobOfferController(IDataUnitOfWork unitOfWork)
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

            var offers = await UnitOfWork.JobOfferRepository.GetFromPageAsync(page, pageSize, "ID");
            return Ok(offers);
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
            try
            {
                await UnitOfWork.JobOfferRepository.AddAsync(entity);
                await UnitOfWork.CompleteAsync();
            }
            catch (DbUpdateException dbe)
            {
                return BadRequest(dbe.Message);
            }

            return Ok(entity);
        }
        [Authorize(Roles = "Contractor")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] JobOffer entity)
        {
            try
            {
                UnitOfWork.JobOfferRepository.Update(entity);
                await UnitOfWork.CompleteAsync();
            }
            catch (DbUpdateException dbe)
            {
                return BadRequest(dbe.Message);
            }

            return Ok(entity);
        }
        [Authorize(Roles = "Customer")]
        [HttpPut("AcceptJobOffer/{offerId}/{customerId}")]
        public async Task<ActionResult> AcceptJobOffer(int offerId, int customerId)
        {
            try
            {
                int jobId=await UnitOfWork.JobOfferRepository.AcceptJobOffer(offerId, customerId);
                return Ok(await UnitOfWork.JobRepository.SingleAsync(j => j.ID == jobId));
            }
            catch (DbUpdateException dbe)
            {
                return BadRequest(dbe.Message);
            }
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
            catch (DbUpdateException dbe)
            {
                return BadRequest(dbe.Message);
            }
            return Ok();
        }
    }
}
