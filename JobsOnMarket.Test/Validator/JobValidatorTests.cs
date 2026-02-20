

using JobMarket.Data.Entity;
using FluentValidation.Results;
using System.Linq;
using JetBrains.Annotations;

namespace JobsOnMarket.Validator.Tests
{
    [TestSubject(typeof(JobValidator))]
    public class JobValidatorTests
    {
        private readonly JobValidator _validator = new JobValidator();

        [Fact]
        public void StartDate_InPast_ShouldHaveValidationError()
        {
            var job = new Job
            {
                StartDate = DateTime.Now.AddMinutes(-10),
                DueDate = DateTime.Now.AddHours(1),
                Description = "Valid description"
            };

            var result = _validator.Validate(job);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(Job.StartDate));
        }

        [Fact]
        public void StartDate_InFuture_ShouldNotHaveValidationError()
        {
            var job = new Job
            {
                StartDate = DateTime.Now.AddMinutes(1),
                DueDate = DateTime.Now.AddHours(1),
                Description = "Valid description"
            };

            var result = _validator.Validate(job);

            Assert.DoesNotContain(result.Errors, e => e.PropertyName == nameof(Job.StartDate));
        }
        [Fact]
        public void StartDate_InFutureButLaterThanDueDate_ShouldHaveValidationError()
        {
            var job = new Job
            {
                StartDate = DateTime.Now.AddHours(2),
                DueDate = DateTime.Now.AddHours(1),
                Description = "invalid description"
            };

            var result = _validator.Validate(job);
            
            Assert.Single(result.Errors);
        }
        [Fact]
        public void DueDate_InPast_ShouldHaveValidationError()
        {
            var job = new Job
            {
                StartDate = DateTime.Now.AddHours(1),
                DueDate = DateTime.Now.AddMinutes(-5),
                Description = "Valid description"
            };

            var result = _validator.Validate(job);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(Job.DueDate));
        }

        [Fact]
        public void DueDate_InFuture_ShouldNotHaveValidationError()
        {
            var job = new Job
            {
                StartDate = DateTime.Now.AddMinutes(1),
                DueDate = DateTime.Now.AddHours(2),
                Description = "Valid description"
            };

            var result = _validator.Validate(job);
            Assert.True(result.IsValid);
            Assert.DoesNotContain(result.Errors, e => e.PropertyName == nameof(Job.DueDate));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Description_NullOrWhitespace_ShouldHaveValidationError(string? desc)
        {
            var job = new Job
            {
                StartDate = DateTime.Now.AddMinutes(1),
                DueDate = DateTime.Now.AddHours(1),
                Description = desc
            };

            var result = _validator.Validate(job);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(Job.Description));
        }

        [Fact]
        public void Description_WithContent_ShouldNotHaveValidationError()
        {
            var job = new Job
            {
                StartDate = DateTime.Now.AddMinutes(1),
                DueDate = DateTime.Now.AddHours(1),
                Description = "Has content"
            };

            var result = _validator.Validate(job);

            Assert.DoesNotContain(result.Errors, e => e.PropertyName == nameof(Job.Description));
        }
    }
}