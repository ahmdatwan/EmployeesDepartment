using EmployeesDepartment.API.Entities;
using EmployeesDepartment.API.Services;

namespace EmployeesDepartment.API.Repository
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<bool> DepartmentExists(int id);
        Task<bool> IsDepartmentEmpty(int id);
    }
}
