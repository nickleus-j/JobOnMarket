using System;
using FluentValidation;
using JobMarket.Data.Entity;
namespace JobsOnMarket.Validator
{
    public class JobOfferValidator : AbstractValidator<JobOffer>
    {
        public JobOfferValidator()
        {
            RuleFor(x => x.Price)
                .InclusiveBetween(0.00, 999999.99)
                .WithMessage("Price must be between 0.00 and 999999.99")
                .Must(HasMaxTwoDecimalPlaces)
                .WithMessage("Price can have at most 2 decimal places");

            RuleFor(x => x.PriceCurrencyId)
                .NotNull()
                .WithMessage("PriceCurrencyId is required")
                .GreaterThan(0)
                .WithMessage("PriceCurrencyId must be greater than 0");

            RuleFor(x => x.JobId)
                .GreaterThan(0)
                .WithMessage("JobId must be greater than 0");

            RuleFor(x => x.OfferedByContractorId)
                .GreaterThan(0)
                .When(x => x.OfferedByContractorId.HasValue)
                .WithMessage("OfferedByContractorId must be greater than 0 when provided");
        }

        private bool HasMaxTwoDecimalPlaces(double price)
        {
            // Convert to decimal to avoid floating-point precision issues then check rounding.
            decimal d;
            try
            {
                d = Convert.ToDecimal(price);
            }
            catch (OverflowException)
            {
                return false;
            }

            decimal rounded = Math.Round(d, 2, MidpointRounding.AwayFromZero);
            return d == rounded;
        }
    }
}
