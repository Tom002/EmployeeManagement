using AutoMapper;
using EmployeeManagement.Bll.Dto;
using EmployeeManagement.Dal.Entities;

namespace EmployeeManagement.Bll.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();

            CreateMap<DepartmentCreateOrUpdateDto, Department>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
                .ForMember(dest => dest.LastModAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastModById, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Employees, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());
        }
    }
}
