using JetBrains.Annotations;
using JobsOnMarket.Dto;
using JobsOnMarket.Validator;

namespace JobsOnMarket.Tests.Validator;

[TestSubject(typeof(RegistrationDtoValidator))]
public class RegistrationDtoValidatorTest
{
    private readonly RegistrationDtoValidator _validator = new();
    [Fact]
    public void ExpectedCustomer_ShouldBeValid()
    {
        var registration = new RegistrationDto
        {
            UserName = "test@test.com", RoleName = "Customer", FirstName = "Test", Surname = "User",UnhashedPassword = "Solicit4%"
        };
        var result = _validator.Validate(registration);
        Assert.True(result.IsValid);
    }
    [Fact]
    public void ExpectedConstractor_ShouldBeValid()
    {
        var registration = new RegistrationDto
        {
            UserName = "test@contractor.com", RoleName = "Contractor", FirstName = "Test", Surname = "User",UnhashedPassword = "Solicit4%"
        };
        var result = _validator.Validate(registration);
        Assert.True(result.IsValid);
    }
    [Fact]
    public void UnexectedRole_ShouldBeInvalid()
    {
        var registration = new RegistrationDto
        {
            UserName = "test@test.com", RoleName = "conman", FirstName = "Test", Surname = "User",UnhashedPassword = "Solicit4%"
        };
        var result = _validator.Validate(registration);
        Assert.False(result.IsValid);
    }
    [Fact]
    public void NullRole_ShouldBeInvalid()
    {
        var registration = new RegistrationDto
        {
            UserName = "test@test.com", RoleName = String.Empty, FirstName = "Test", Surname = "User",UnhashedPassword = "Solicit4%"
        };
        var result = _validator.Validate(registration);
        Assert.False(result.IsValid);
    }
}
