using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesDepartment.API.Entities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        public string Job { get; set; }
        public double Salary { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }

        public Employee(string name, string job)
        {
            Name = name;
            Job = job;
        }
    }
}
