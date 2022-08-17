using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesDepartment.API.Entities
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Location { get; set; }
        public ICollection<Employee> Employees { get; set;} = new List<Employee>();
        public Department(string name)
        {
            Name = name;
           

        }
    }
}
