using EmployeesDepartment.API.Entities;
using EmployeesDepartment.API.Services;

namespace EmployeesDepartment.API.Repository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetDepartmentEmployees(int id );
        void Insert( Employee employee, Department department );
    }
}
