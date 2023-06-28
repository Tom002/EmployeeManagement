using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeeManagement.Bll.Dto;
using EmployeeManagement.Common.Exceptions;
using EmployeeManagement.Dal;
using EmployeeManagement.Dal.Entities;
using EmployeeManagement.Dal.Repository;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Bll.Services
{
    public class EmployeeService : LogicService<Employee>, IEmployeeService
    {
        private readonly IMapper _mapper;

        public EmployeeService(EmployeeManagementDbContext dbContext, IMapper mapper) 
            : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task<List<EmployeeListDto>> ListEmployees()
        {
            return await Query()
                .ProjectTo<EmployeeListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<EmployeeDetailsDto> GetEmployeeDetails(long employeeId)
        {
            return await Query()
                .ProjectTo<EmployeeDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == employeeId)
                ?? throw new EntityNotFoundException($"Cannot find entity of type {typeof(Employee).Name} with id={employeeId}");
        }

        public async Task AddEmployee(EmployeeCreateDto createDto)
        {
            var employee = _mapper.Map<Employee>(createDto);

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(createDto.Password);
            employee.PasswordHash = passwordHash;

            await AddAsync(employee);
        }

        public async Task UpdateEmployee(int employeeId, EmployeeUpdateDto updateDto)
        {
            var employee = await GetByIdAsync(employeeId);

            if (employeeId == updateDto.BossId)
                throw new BusinessException("An employee can't be his own boss");

            _mapper.Map(updateDto, employee);
            await UpdateAsync(employee);
        }

        public async Task DeleteEmployee(long employeeId)
        {
            var employee = await GetByIdAsync(employeeId);
            await DeleteAsync(employee);
        }
    }
}
