using EmployeesDepartment.API.DbContexts;
using EmployeesDepartment.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesDepartment.API.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DepartmentContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetDepartmentEmployees( int id )
        {

            return await _context.Employees.Where(employee => employee.DepartmentId == id).ToListAsync(); 
        }

        public void Insert(Employee employee, Department department)
        {
           department.Employees.Add(employee);
        }
    }
}
