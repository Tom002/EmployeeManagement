using EmployeeManagement.Bll.Dto;
using FluentValidation;

namespace EmployeeManagement.Bll.Validators
{
    public class EmployeeUpdateDtoValidator : AbstractValidator<EmployeeUpdateDto>
    {
        public EmployeeUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Position).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(16).Matches("^((\\+36-?))?[0-9]{8,10}$");
            RuleFor(x => x.DepartmentId).NotEmpty().GreaterThanOrEqualTo(1);
        }
    }
}
