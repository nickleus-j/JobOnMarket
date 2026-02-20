using JobsOnMarket.Dto;
using FluentValidation;

namespace JobsOnMarket.Validator;

public class ChangePasswordDtoValidator: AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.OldPassword)
            .Must(TextHasNonWhiteSpaceContent).WithMessage("Password must have non-white space characters");
        RuleFor(x => x.NewPassword).Must(TextHasNonWhiteSpaceContent)
            .WithMessage("Password must have non-white space characters");
        RuleFor(x=>x).Must(OldPasswordIsDifferentFromNewPassword)
            .WithMessage("Old password and new password are different regardless of capitalization");
    }
    private bool TextHasNonWhiteSpaceContent(string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    private bool OldPasswordIsDifferentFromNewPassword(ChangePasswordDto dto)
    {
        return !String.Equals(dto.OldPassword, dto.NewPassword, StringComparison.InvariantCultureIgnoreCase);
    }
}