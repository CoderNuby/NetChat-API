using FluentValidation;

namespace Application.Validators
{
    public static class ValidatorExtension
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder.NotEmpty().MinimumLength(6).WithMessage("password must be at least 6 lenght")
                .Matches("[A-Z]").WithMessage("password must contain at least 1 capital letter")
                .Matches("[0-9]").WithMessage("password must contain at least 1 number")
                .Matches("[^a-z-A-Z-0-9]").WithMessage("password must contain non alphanumeric character");

            return options;
        }
    }
}
