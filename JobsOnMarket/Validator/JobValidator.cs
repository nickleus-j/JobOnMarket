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
        }

        private bool BeNowOrInTheFuture(DateTime date)
        {
            return date >= DateTime.Now;
        }
    }
}
