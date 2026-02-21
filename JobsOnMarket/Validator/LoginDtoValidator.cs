using FluentValidation;
using JobsOnMarket.Dto;
namespace JobsOnMarket.Validator
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName)
                .Must(MustNotBeEmpty)
                .WithMessage("UserName must not be empty nor WhiteSpace");
            RuleFor(x => x.UserName).EmailAddress().WithMessage("UserName must be a valid email address");
            RuleFor(x => x.UnhashedPassword)
                .Must(TextMustBeAtLeast6Chars)
                .WithMessage("Password Must have at least 6 Characters");
        }

        private bool MustNotBeEmpty(string text)
        {
            return !String.IsNullOrWhiteSpace(text);
        }
        private bool TextMustBeAtLeast6Chars(string? text)
        {
            return !String.IsNullOrWhiteSpace(text) && text.Length >= 6;
        }
    }
}
