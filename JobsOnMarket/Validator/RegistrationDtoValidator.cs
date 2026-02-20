using FluentValidation;
using JobsOnMarket.Dto;

namespace JobsOnMarket.Validator;

public class RegistrationDtoValidator: AbstractValidator<RegistrationDto>
{
    public RegistrationDtoValidator()
    {
        RuleFor(x => x.UserName).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Username is required");
        RuleFor(x => x.UserName).EmailAddress().WithMessage("UserName must be a valid email address");
        RuleFor(x => x.UnhashedPassword).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Password is required");
        RuleFor(x=>x.FirstName).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("First name is required");
        RuleFor(x=>x.Surname).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Surname is required");
        RuleFor(x=>x.RoleName).Must(expectedRole).WithMessage("Unexpected Role name");
        RuleFor(x => x.RoleName).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Role Name is required");
    }

    private bool expectedRole(string roleName)
    {
        string lowerCaseRole=roleName.ToLower();
        IList<string> roleNames = ["customer","contractor"];
        return roleNames.Contains(lowerCaseRole.ToLower());
    }
}