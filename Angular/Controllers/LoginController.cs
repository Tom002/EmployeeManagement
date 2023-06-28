using EmployeeManagement.Bll.Dto;
using EmployeeManagement.Bll.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public LoginController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            var employee = await _employeeService
                .Query(x => x.Username == loginDto.Username)
                .FirstOrDefaultAsync();

            if(employee == null)
                return Problem("Invalid credentials", statusCode: 400);

            if(!BCrypt.Net.BCrypt.Verify(loginDto.Password, employee.PasswordHash))
                return Problem("Invalid credentials", statusCode: 400);

            var token = TokenHelper.CreateAccessToken(employee);
            var response = new LoginResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Token = token
            };
            return Ok(response);
        }
    }
}
