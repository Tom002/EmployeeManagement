using EmployeeManagement.Bll.Dto;
using EmployeeManagement.Dal.Entities;
using EmployeeManagement.Dal.Repository;

namespace EmployeeManagement.Bll.Services
{
    public interface IDepartmentService : ILogicService<Department>
    {
        Task AddDepartment(DepartmentCreateOrUpdateDto createDto);
        Task DeleteDepartment(long departmentId);
        Task<DepartmentDto> GetDepartment(long departmentId);
        Task<List<DepartmentDto>> ListDepartments();
        Task UpdateDepartment(int id, DepartmentCreateOrUpdateDto updateDto);
    }
}
