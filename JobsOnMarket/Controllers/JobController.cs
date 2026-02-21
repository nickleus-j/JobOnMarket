using Azure.Core;
using FluentValidation;
using JobMarket.Data;
using JobMarket.Data.Entity;
using JobMarket.Ef.Util;
using JobsOnMarket.Dto.Job;
using JobsOnMarket.Mappers;
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
        private  IValidator<JobDto> _dtoValidator;
        public JobController(IDataUnitOfWork unitOfWork, IValidator<JobDto> validator)
        {
            this.UnitOfWork = unitOfWork;
            this._dtoValidator = validator;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Page and pageSize must be greater than 0.");
            }

            var jobs = await UnitOfWork.JobRepository.GetFromPageAsync(page, pageSize, "ID");
            var dtoList= JobMapper.MapToDtos(jobs,await UnitOfWork.CurrencyRepository.GetAllAsync());
            UnitOfWork.Dispose();
            return Ok(dtoList);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var job=await UnitOfWork.JobRepository.GetAsync(id);
            var dto= JobMapper.MapToDto(job,await UnitOfWork.CurrencyRepository.FindAsync(c=>c.Id==job.BudgetCurrencyId));
            UnitOfWork.Dispose();
            return Ok(dto);
        }
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] JobDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns 400 Bad Request with error details
            }
            var validationResult = await _dtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }
            try
            {
                var entity= JobMapper.MapToJob(dto, new CurrencyLister().GetCurrencies());
                entity.BudgetCurrency = null;
                await UnitOfWork.JobRepository.AddAsync(entity);
                await UnitOfWork.CompleteAsync();
            }
            catch (DbUpdateException dbe)
            {
                return BadRequest(dbe.Message);
            }

            return Ok(dto);
        }
        [Authorize(Roles = "Customer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] JobDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns 400 Bad Request with error details
            }
            
            try
            {
                var currencies = await UnitOfWork.CurrencyRepository.FindAsync(c =>
                    String.Equals(c.Code.ToLower(), dto.CurrencyCode.ToLower()));
                var entity = JobMapper.MapToJob(dto, currencies);
                UnitOfWork.JobRepository.Update(entity);
                await UnitOfWork.CompleteAsync();
            }
            catch (DbUpdateException dbe)
            {
                return BadRequest(dbe.Message);
            }
            return Ok(dto);
        }
        [Authorize(Roles = "Customer")]
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
