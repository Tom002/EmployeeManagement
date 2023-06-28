using EmployeeManagement.Bll.Dto;
using FluentValidation;

namespace EmployeeManagement.Bll.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
