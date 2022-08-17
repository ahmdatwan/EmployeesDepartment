using AutoMapper;

namespace EmployeesDepartment.API.Profiles
{
    public class DepartmentProfile :Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Entities.Department, Models.DepartmentDTO>();
            CreateMap<Models.DepartmentForCreationDTO, Entities.Department>();
            CreateMap< Models.DepartmentDTO,Entities.Department> ();
            CreateMap<Entities.Department,Models.DepartmentForCreationDTO> ();
        }
    }
}
