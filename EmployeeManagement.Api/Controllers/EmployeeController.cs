using EmployeeManagement.Bll.Dto;
using EmployeeManagement.Bll.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public Task<List<EmployeeListDto>> ListEmployees()
            => _employeeService.ListEmployees();

        [HttpGet("{employeeId:int}")]
        public Task<EmployeeDetailsDto> GetEmployee([FromRoute] int employeeId)
            => _employeeService.GetEmployeeDetails(employeeId);

        [HttpPost]
        public Task AddEmployee([FromBody] EmployeeCreateDto dto) 
            => _employeeService.AddEmployee(dto);

        [HttpPut("{employeeId:int}")]
        public Task UpdateEmployee([FromRoute] int employeeId, [FromBody] EmployeeUpdateDto dto)
            => _employeeService.UpdateEmployee(employeeId, dto);

        [HttpDelete("{employeeId:int}")]
        public Task DeleteEmployee([FromRoute] int employeeId)
            => _employeeService.DeleteEmployee(employeeId);
    }
}
