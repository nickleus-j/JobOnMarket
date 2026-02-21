using FluentValidation;
using JobMarket.Data.Entity;
using JobMarket.Ef.Util;
using JobsOnMarket.Dto.Job;

namespace JobsOnMarket.Validator
{
    public class JobValidator : AbstractValidator<JobDto>
    {
        public JobValidator()
        {
            RuleFor(x => x.StartDate)
                .Must(BeNowOrInTheFuture)
                .WithMessage("The event start date must be now or in the future.");
            RuleFor(x => x.DueDate)
                .Must(BeNowOrInTheFuture)
                .WithMessage("The event due date must be now or in the future.");
            RuleFor(x => x.Description)
                .Must(TextHasContent)
                .WithMessage("Text must have Content");
            RuleFor(x=>x)
                .Must(DueDateLaterThanStartDate)
                .WithMessage("The event due date must be later than the current date.");
            RuleFor(x => x)
                .Must(x=>!String.IsNullOrWhiteSpace(x.CurrencyCode))
                .WithMessage("Currency Id must have value");
            RuleFor(x => x.CurrencyCode)
                .Must(ValidCurrnecyId)
                .WithMessage("Not  valid Currency ID");
        }

        private bool BeNowOrInTheFuture(DateTime date)
        {
            return date >= DateTime.Now;
        }
        private bool DueDateLaterThanStartDate(JobDto job)
        {
            return job.DueDate >= job.StartDate;
        }
        private bool TextHasContent(string? text)
        {
            return !String.IsNullOrWhiteSpace(text);
        }
        private bool ValidCurrnecyId(string CurrencyCode)
        {
            CurrencyLister currencyLister = new CurrencyLister();
            var currencies = currencyLister.GetCurrencies().Select(x => x.Code.ToUpper()).ToList();
            return currencies.Any(c => c == CurrencyCode.ToUpper());
        }
    }
}
