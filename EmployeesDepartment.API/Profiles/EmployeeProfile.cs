using AutoMapper;

namespace EmployeesDepartment.API.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Entities.Employee,Models.EmployeeDTO>();
            CreateMap<Models.EmployeeDTO,Entities.Employee>();
            CreateMap<Models.EmployeeForCreationDTO,Entities.Employee>();
            CreateMap<Entities.Employee,Models.EmployeeForCreationDTO > ();

        }
    }
}
