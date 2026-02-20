using System.Linq;
using Xunit;
using JobsOnMarket.Dto;
using JobsOnMarket.Validator;

namespace JobsOnMarket.Tests.Validator
{
    public class LoginDtoValidatorTests
    {
        private readonly LoginDtoValidator _validator = new();

        [Fact]
        public void Validate_ValidDto_IsValid()
        {
            var dto = new LoginDto
            {
                UserName = "jane",
                UnhashedPassword = "secret1"
            };

            var result = _validator.Validate(dto);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_EmptyUserName_FailsWithUserNameMessage()
        {
            var dto = new LoginDto
            {
                UserName = "",
                UnhashedPassword = "secret1"
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
            var expected = "UserName must nopt be empty nor WhiteSpace";
            Assert.True(result.Errors.Any(e => e.PropertyName == nameof(dto.UserName) && e.ErrorMessage == expected),
                $"Expected a validation error for {nameof(dto.UserName)} with message: {expected}");
        }

        [Fact]
        public void Validate_WhitespaceUserName_FailsWithUserNameMessage()
        {
            var dto = new LoginDto
            {
                UserName = "   ",
                UnhashedPassword = "secret1"
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
            var expected = "UserName must nopt be empty nor WhiteSpace";
            Assert.True(result.Errors.Any(e => e.PropertyName == nameof(dto.UserName) && e.ErrorMessage == expected),
                $"Expected a validation error for {nameof(dto.UserName)} with message: {expected}");
        }

        [Fact]
        public void Validate_PasswordTooShort_FailsWithPasswordMessage()
        {
            var dto = new LoginDto
            {
                UserName = "john",
                UnhashedPassword = "12345" // 5 chars, less than 6
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
            var expected = "Password Must have at least 6 Characters";
            Assert.True(result.Errors.Any(e => e.PropertyName == nameof(dto.UnhashedPassword) && e.ErrorMessage == expected),
                $"Expected a validation error for {nameof(dto.UnhashedPassword)} with message: {expected}");
        }

        [Fact]
        public void Validate_NullPassword_FailsWithPasswordMessage()
        {
            var dto = new LoginDto
            {
                UserName = "Doe",
                UnhashedPassword = null
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
            var expected = "Password Must have at least 6 Characters";
            Assert.True(result.Errors.Any(e => e.PropertyName == nameof(dto.UnhashedPassword) && e.ErrorMessage == expected),
                $"Expected a validation error for {nameof(dto.UnhashedPassword)} with message: {expected}");
        }
    }
}