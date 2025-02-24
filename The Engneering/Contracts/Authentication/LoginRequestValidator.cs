namespace The_Engneering.Contracts.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress();

        RuleFor(l => l.Password)
             .NotEmpty().WithMessage("Password is required.");
             //.MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
             //.Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
             //.Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
             //.Matches(@"\d").WithMessage("Password must contain at least one number.")
             //.Matches(@"[\W_]").WithMessage("Password must contain at least one special character.")
    }
}
