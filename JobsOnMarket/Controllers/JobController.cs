using Azure.Core;
using FluentValidation;
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
        private IDataUnitOfWork UnitOfWork;
        private  IValidator<Job> _validator;
        public JobController(IDataUnitOfWork unitOfWork, IValidator<Job> validator)
        {
            this.UnitOfWork = unitOfWork;
            this._validator = validator;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Page and pageSize must be greater than 0.");
            }

            var jobs = await UnitOfWork.JobRepository.GetFromPageAsync(page, pageSize, "ID");
            return Ok(jobs);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns 400 Bad Request with error details
            }
            var validationResult = await _validator.ValidateAsync(entity);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }
            try
            {
                await UnitOfWork.JobRepository.AddAsync(entity);
                await UnitOfWork.CompleteAsync();
            }
            catch (DbUpdateException dbe)
            {
                return BadRequest(dbe.Message);
            }

            return Ok(entity);
        }
        [Authorize(Roles = "Customer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Job entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns 400 Bad Request with error details
            }
            var validationResult = await _validator.ValidateAsync(entity);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }
            try
            {
                UnitOfWork.JobRepository.Update(entity);
                await UnitOfWork.CompleteAsync();
            }
            catch (DbUpdateException dbe)
            {
                return BadRequest(dbe.Message);
            }
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
            catch (DbUpdateException dbe)
            {
                return BadRequest(dbe.Message);
            }
            return Ok();
        }
    }
}
