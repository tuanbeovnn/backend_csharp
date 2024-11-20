using FluentValidation;

namespace Dtos.Accounts;
public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(e => e.Username)
            .NotEmpty().WithMessage("Tên đăng nhập không được để trống");
        RuleFor(e => e.Password)
            .NotEmpty().WithMessage("Mật khẩu không được để trống");
    }
}
