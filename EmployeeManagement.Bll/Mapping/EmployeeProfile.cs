using AutoMapper;
using EmployeeManagement.Bll.Dto;
using EmployeeManagement.Dal.Entities;

namespace EmployeeManagement.Bll.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeListDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<Employee, EmployeeDetailsDto>()
                .IncludeBase<Employee, EmployeeListDto>()
                .ForMember(dest => dest.BossName, opt => opt.MapFrom(src => src.Boss.Name));

            CreateMap<EmployeeCreateDto, Employee>()
                .ForMember(dest => dest.Boss, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
                .ForMember(dest => dest.LastModAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastModById, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Subordinates, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());

            CreateMap<EmployeeUpdateDto, Employee>()
                .ForMember(dest => dest.Boss, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
                .ForMember(dest => dest.LastModAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastModById, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Subordinates, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.Username, opt => opt.Ignore());
        }
    }
}
