using EmployeeManagement.Bll.Dto;
using FluentValidation;

namespace EmployeeManagement.Bll.Validators
{
    public class DepartmentCreateOrUpdateDtoValidator : AbstractValidator<DepartmentCreateOrUpdateDto>
    {
        public DepartmentCreateOrUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Abbreviation).NotEmpty().MaximumLength(5);
        }
    }
}
