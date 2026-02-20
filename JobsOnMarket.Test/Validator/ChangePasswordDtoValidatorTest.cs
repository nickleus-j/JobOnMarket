using JetBrains.Annotations;
using JobsOnMarket.Dto;
using JobsOnMarket.Validator;

namespace JobsOnMarket.Tests.Validator;

[TestSubject(typeof(ChangePasswordDtoValidator))]
public class ChangePasswordDtoValidatorTest
{
    private readonly ChangePasswordDtoValidator _validator = new ChangePasswordDtoValidator();
    [Fact]
    public void PasswordsDontMatch_ShouldHaveNoValidationError()
    {
        var changePasswordSampleDto = new ChangePasswordDto
        {
            OldPassword="thinkpad-T14",
            NewPassword = "MacBook Air13"
        };
        var result = _validator.Validate(changePasswordSampleDto);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    [Fact]
    public void PasswordsMatch_ShouldHaveValidationError()
    {
        var changePasswordSampleDto = new ChangePasswordDto
        {
            OldPassword="thinkpad-T14",
            NewPassword = "thinkpad-T14"
        };
        var result = _validator.Validate(changePasswordSampleDto);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
    }
    [Fact]
    public void PasswordsMatchDifferentCapitalization_ShouldHaveValidationError()
    {
        var changePasswordSampleDto = new ChangePasswordDto
        {
            OldPassword="Thinkpad-T14",
            NewPassword = "thinkpad-T14"
        };
        var result = _validator.Validate(changePasswordSampleDto);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
    }
    [Fact]
    public void OldPasswordEmpty_ShouldHaveValidationError()
    {
        var changePasswordSampleDto = new ChangePasswordDto
        {
            OldPassword=String.Empty,
            NewPassword = "MacBook Air13"
        };
        var result = _validator.Validate(changePasswordSampleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ChangePasswordDto.OldPassword));
    }
    [Fact]
    public void NewPasswordEmpty_ShouldHaveValidationError()
    {
        var changePasswordSampleDto = new ChangePasswordDto
        {
            OldPassword="alt+F4",
            NewPassword = ""
        };
        var result = _validator.Validate(changePasswordSampleDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ChangePasswordDto.NewPassword));
    }
}