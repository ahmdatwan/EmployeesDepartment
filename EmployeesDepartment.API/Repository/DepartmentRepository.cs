using EmployeesDepartment.API.DbContexts;
using EmployeesDepartment.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesDepartment.API.Repository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DepartmentContext context) : base(context)
        {

        }
        public async Task<bool> DepartmentExists(int id)
        {
            return await _context.Department.AnyAsync(department => department.Id == id );
        }

        public async Task<IEnumerable<Employee>> GetEmployees(int id)
        {
            return await _context.Employees.Where(employee => employee.DepartmentId == id).ToListAsync();
        }

        public async Task<bool> IsDepartmentEmpty(int id)
        {
            return await _context.Employees.AnyAsync(emp => emp.DepartmentId == id);
            
        }
    }
}
