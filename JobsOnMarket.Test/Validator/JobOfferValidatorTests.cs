using System.Linq;
using FluentValidation.Results;
using JobsOnMarket.Validator;
using JobMarket.Data.Entity;
using Xunit;

namespace JobsOnMarket.Tests.Validator
{
    public class JobOfferValidatorTests
    {
        private readonly JobOfferValidator _validator = new JobOfferValidator();

        [Fact]
        public void ValidOffer_AllValidProperties_IsValid()
        {
            var model = new JobOffer
            {
                Price = 1000.00,
                PriceCurrencyId = 1,
                JobId = 1,
                OfferedByContractorId = null
            };

            ValidationResult result = _validator.Validate(model);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void PriceWithMoreThanTwoDecimalPlaces_IsInvalid()
        {
            var model = new JobOffer
            {
                Price = 1.234, // more than 2 decimal places
                PriceCurrencyId = 1,
                JobId = 1
            };

            ValidationResult result = _validator.Validate(model);

            Assert.False(result.IsValid);
            var error = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(JobOffer.Price));
            Assert.NotNull(error);
            Assert.Contains("at most 2 decimal places", error.ErrorMessage);
        }

        [Theory]
        [InlineData(-1.0)]
        [InlineData(1000000.00)]
        public void PriceOutOfRange_IsInvalid(double price)
        {
            var model = new JobOffer
            {
                Price = price,
                PriceCurrencyId = 1,
                JobId = 1
            };

            ValidationResult result = _validator.Validate(model);

            Assert.False(result.IsValid);
            var error = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(JobOffer.Price));
            Assert.NotNull(error);
            Assert.Contains("Price must be between 0.00 and 999999.99", error.ErrorMessage);
        }

        [Fact]
        public void PriceCurrencyIdZero_IsInvalid()
        {
            var model = new JobOffer
            {
                Price = 10.0,
                PriceCurrencyId = 0, // must be > 0
                JobId = 1
            };

            ValidationResult result = _validator.Validate(model);

            Assert.False(result.IsValid);
            var error = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(JobOffer.PriceCurrencyId));
            Assert.NotNull(error);
            Assert.Contains("PriceCurrencyId must be greater than 0", error.ErrorMessage);
        }

        [Fact]
        public void JobIdZero_IsInvalid()
        {
            var model = new JobOffer
            {
                Price = 10.0,
                PriceCurrencyId = 1,
                JobId = 0 // must be > 0
            };

            ValidationResult result = _validator.Validate(model);

            Assert.False(result.IsValid);
            var error = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(JobOffer.JobId));
            Assert.NotNull(error);
            Assert.Contains("JobId must be greater than 0", error.ErrorMessage);
        }

        [Fact]
        public void OfferedByContractorId_Negative_WhenProvided_IsInvalid()
        {
            var model = new JobOffer
            {
                Price = 10.0,
                PriceCurrencyId = 1,
                JobId = 1,
                OfferedByContractorId = -5 // provided but invalid
            };

            ValidationResult result = _validator.Validate(model);

            Assert.False(result.IsValid);
            var error = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(JobOffer.OfferedByContractorId));
            Assert.NotNull(error);
            Assert.Contains("OfferedByContractorId must be greater than 0", error.ErrorMessage);
        }

        [Fact]
        public void OfferedByContractorId_Null_IsValid()
        {
            var model = new JobOffer
            {
                Price = 50.0,
                PriceCurrencyId = 1,
                JobId = 1,
                OfferedByContractorId = null // not provided
            };

            ValidationResult result = _validator.Validate(model);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
    }
}
