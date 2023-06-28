using EmployeeManagement.Bll.Dto;
using EmployeeManagement.Bll.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public Task<List<DepartmentDto>> ListDepartments()
            => _departmentService.ListDepartments();

        [HttpGet("{departmentId:int}")]
        public Task<DepartmentDto> GetDepartment([FromRoute] int departmentId)
            => _departmentService.GetDepartment(departmentId);

        [HttpPost]
        public Task AddDepartment([FromBody] DepartmentCreateOrUpdateDto dto)
            => _departmentService.AddDepartment(dto);

        [HttpPut("{departmentId:int}")]
        public Task UpdateDepartment([FromRoute] int departmentId, [FromBody] DepartmentCreateOrUpdateDto dto)
            => _departmentService.UpdateDepartment(departmentId, dto);

        [HttpDelete("{departmentId:int}")]
        public Task DeleteDepartment([FromRoute] int departmentId)
            => _departmentService.DeleteDepartment(departmentId);
    }
}
