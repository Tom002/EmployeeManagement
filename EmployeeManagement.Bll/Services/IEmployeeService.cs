using EmployeeManagement.Bll.Dto;
using EmployeeManagement.Dal.Entities;
using EmployeeManagement.Dal.Repository;

namespace EmployeeManagement.Bll.Services
{
    public interface IEmployeeService : ILogicService<Employee>
    {
        Task AddEmployee(EmployeeCreateDto createDto);
        Task DeleteEmployee(long employeeId);
        Task<EmployeeDetailsDto> GetEmployeeDetails(long employeeId);
        Task<List<EmployeeListDto>> ListEmployees();
        Task UpdateEmployee(int id, EmployeeUpdateDto updateDto);
    }
}
