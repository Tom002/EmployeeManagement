using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeeManagement.Bll.Dto;
using EmployeeManagement.Common.Exceptions;
using EmployeeManagement.Common.RequestContext;
using EmployeeManagement.Dal;
using EmployeeManagement.Dal.Entities;
using EmployeeManagement.Dal.Repository;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Bll.Services
{
    public class DepartmentService : LogicService<Department>, IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly IRequestContext _requestContext;

        public DepartmentService(EmployeeManagementDbContext dbContext, IMapper mapper, IRequestContext requestContext)
            : base(dbContext)
        {
            _mapper = mapper;
            _requestContext = requestContext;
        }

        public async Task<List<DepartmentDto>> ListDepartments()
        {
            return await Query()
                .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(_requestContext.RequestAborted);
        }

        public async Task<DepartmentDto> GetDepartment(long departmentId)
        {
            return await Query()
                .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == departmentId, _requestContext.RequestAborted)
                ?? throw new EntityNotFoundException($"Cannot find entity of type {typeof(Department).Name} with id={departmentId}");
        }

        public async Task AddDepartment(DepartmentCreateOrUpdateDto createDto)
        {
            var department = _mapper.Map<Department>(createDto);
            await AddAsync(department);
        }

        public async Task UpdateDepartment(int id, DepartmentCreateOrUpdateDto updateDto)
        {
            var department = await GetByIdAsync(id);

            _mapper.Map(updateDto, department);
            await UpdateAsync(department);
        }

        public async Task DeleteDepartment(long departmentId)
        {
            var department = await GetByIdAsync(departmentId);
            await DeleteAsync(department);
        }
    }
}
