using AutoMapper;
using Company.Data.Models;
using Company.Common;

namespace Company.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company.Data.Models.Company, CompanyDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<JobTitle, JobTitleDto>();
            CreateMap<EmployeeJobTitle, EmployeeJobTitleDto>();

            CreateMap<CompanyDto, Company.Data.Models.Company>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<JobTitleDto, JobTitle>();
            CreateMap<EmployeeJobTitleDto, EmployeeJobTitle>();
        }
    }
}
