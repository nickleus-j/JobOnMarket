using JobMarket.Data;
using JobMarket.Data.Entity;
using JobsOnMarket.Mappers;
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
        private readonly UserManager<IdentityUser> _userManager;
        public JobOfferController(IDataUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            this.UnitOfWork = unitOfWork;
            _userManager = userManager;
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
                var contractor = await UnitOfWork.ContractorRepository.GetContractorByUserIdAsync(User.Identity.Name);
                entity.OfferedByContractorId = contractor.ID;
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
        [HttpPut("AcceptJobOffer/{offerId}")]
        public async Task<ActionResult> AcceptJobOffer(int offerId)
        {
            try
            {
                var customer = await UnitOfWork.CustomerRepository.GetCustomerByUserNameAsync(User.Identity.Name);
                int jobId=await UnitOfWork.JobOfferRepository.AcceptJobOffer(offerId, customer.ID);
                Job job = await UnitOfWork.JobRepository.SingleAsync(j => j.ID == jobId);
                var dto = JobMapper.MapToDto(job,
                    await UnitOfWork.CurrencyRepository.FindAsync(c => c.Id == job.BudgetCurrencyId));
                UnitOfWork.Dispose();
                return Ok(dto);
            }
            catch (DbUpdateException dbe)
            {
                return BadRequest(dbe.Message);
            }
        }
        [Authorize(Roles = "Contractor")]
        [HttpDelete()]
        public async Task<ActionResult> Delete([FromBody] JobOffer entity)
        {
            try
            {
                UnitOfWork.JobOfferRepository.Remove(entity);
                await UnitOfWork.CompleteAsync();
                UnitOfWork.Dispose();
            }
            catch (DbUpdateException dbe)
            {
                return BadRequest(dbe.Message);
            }
            return Ok();
        }
    }
}
