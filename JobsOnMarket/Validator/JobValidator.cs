using FluentValidation;
using JobMarket.Data.Entity;
namespace JobsOnMarket.Validator
{
    public class JobValidator : AbstractValidator<Job>
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
        }

        private bool BeNowOrInTheFuture(DateTime date)
        {
            return date >= DateTime.Now;
        }
        private bool DueDateLaterThanStartDate(Job job)
        {
            return job.DueDate >= job.StartDate;
        }
        private bool TextHasContent(string? text)
        {
            return !String.IsNullOrWhiteSpace(text);
        }
    }
}
