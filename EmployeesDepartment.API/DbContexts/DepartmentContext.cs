using EmployeesDepartment.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesDepartment.API.DbContexts
{
    public class DepartmentContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Department> Department { get; set; } = null!;

        public DepartmentContext(DbContextOptions<DepartmentContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                new Department("Frontend") { Id = 1 , Location="C5.302"},
                new Department("Backend") { Id = 2 },
                new Department("Quality Control") { Id = 3 }
                );
            modelBuilder.Entity<Employee>().HasData(
                new Employee("Ahmed Amr", "Intern")
                {
                    Id = 1,
                    Salary = 3000,
                    DepartmentId = 2
                },
                new Employee("Ahmed Mostafa", "Project Lead")
                {
                    Id=2,
                    DepartmentId=2
                }
                ) ;
            base.OnModelCreating(modelBuilder);
        }
    }
}
